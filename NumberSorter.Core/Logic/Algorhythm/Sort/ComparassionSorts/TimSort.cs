using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class TimSort<T> : GenericSortAlgorhythm<T>
    {
        private const int _minrun = 32;

        private IRunMergerFactory RunMergerFactory { get; }
        private ISortAlgorhythm<T> MinrunSortAlgorhythm { get; }  // Used when sorting something smaller then Minrun

        public TimSort(IComparer<T> comparer, IRunMergerFactory runMergerFactory, ISortFactory minrunSortFactory) : base(comparer)
        {
            RunMergerFactory = runMergerFactory;
            MinrunSortAlgorhythm = minrunSortFactory.GetSort(comparer);
        }
        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            if (list.Count < _minrun)
            {
                MinrunSortAlgorhythm.Sort(list);
                return;
            }

            var runMerger = RunMergerFactory.GetMerger(Comparer, list);

            int currentIndex = startingIndex;
            int elementsLeft = length;
            int minimalRunLength = GetMinrun(length);
            while (elementsLeft > 0)
            {
                var sortRun = FindNextSortRun(list, currentIndex);
                if (sortRun.Length < minimalRunLength)
                {
                    sortRun = new SortRun(sortRun.Start, Math.Min(minimalRunLength, elementsLeft));
                    MinrunSortAlgorhythm.Sort(list, sortRun.Start, sortRun.Length);
                }

                runMerger.Push(sortRun);

                elementsLeft -= sortRun.Length;
                currentIndex += sortRun.Length;
            }

            runMerger.ForceMerge();
        }

        private SortRun FindNextSortRun(IList<T> list, int runStart)
        {
            int runLength = 0;
            int listSize = list.Count;
            int currentIndex = runStart;

            int previousRunDirection = 0;
            var previousElement = list[runStart];

            while (currentIndex < listSize)
            {
                var nextElement = list[currentIndex];
                var runDirection = Math.Sign(Compare(previousElement, nextElement));
                if (previousRunDirection == 0 || previousRunDirection == runDirection)
                {
                    previousRunDirection = runDirection;
                    previousElement = nextElement;
                    currentIndex++;
                    runLength++;
                }
                else
                {
                    break;
                }
            }

            return GetSortRun(list, runStart, runLength, previousRunDirection);
        }

        private static SortRun GetSortRun(IList<T> list, int runStart, int runLength, int runDirection)
        {
            var sortRun = new SortRun(runStart, runLength);
            if (runDirection > 0)
                SortRunUtility.InvertRun(list, sortRun);
            return sortRun;
        }

        private static int GetMinrun(int n)
        {
            int r = 0;
            while (n >= _minrun)
            {
                r |= n & 1;
                n >>= 1;
            }
            return n + r;
        }
    }
}
