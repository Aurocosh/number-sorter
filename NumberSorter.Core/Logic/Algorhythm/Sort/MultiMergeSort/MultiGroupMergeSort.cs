using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class MultiGroupMergeSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        private int MinimalRunLength { get; }
        private IPartialSortAlgorhythm<T> GroupSortAlgorhythm { get; }
        private Func<IComparer<SortRun>, IPartialSortAlgorhythm<SortRun>> RunSortFactory { get; }

        public MultiGroupMergeSort(IComparer<T> comparer, Func<IComparer<SortRun>, IPartialSortAlgorhythm<SortRun>> runSortFactory, Func<IComparer<T>, IPartialSortAlgorhythm<T>> groupSortAlgorhythmFactory, int minRunLength = 32) : base(comparer)
        {
            MinimalRunLength = minRunLength;
            RunSortFactory = runSortFactory;
            GroupSortAlgorhythm = groupSortAlgorhythmFactory.Invoke(comparer);
        }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            if (length < 2)
                return;

            var sortRuns = FindSortRuns(list, startingIndex, length);
            var comparer = new FirstRunElementComparer<T>(list, GetComparer());
            var runSorter = RunSortFactory.Invoke(comparer);

            runSorter.Sort(sortRuns);

            var temporartArray = list.GetRangeAsArray(startingIndex, length);

            int currentRunIndex = 0;
            int runIndexLimit = sortRuns.Count;

            int index = startingIndex;

            while (currentRunIndex != runIndexLimit)
            {
                var lowestRun = sortRuns[currentRunIndex];
                list[index++] = temporartArray[lowestRun.FirstIndex];

                var newRun = new SortRun(lowestRun.FirstIndex + 1, lowestRun.Length - 1);
                sortRuns[currentRunIndex] = newRun;

                if (newRun.Length == 0)
                {
                    currentRunIndex++;
                    continue;
                }

                int firstIndex = currentRunIndex;
                int secondIndex = currentRunIndex + 1;
                while (secondIndex != runIndexLimit && Compare(temporartArray[sortRuns[firstIndex].FirstIndex], temporartArray[sortRuns[secondIndex].FirstIndex]) > 0)
                {
                    sortRuns.Swap(firstIndex, secondIndex);
                    firstIndex++;
                    secondIndex++;
                }
            }
        }

        List<SortRun> FindSortRuns(IList<T> list, int startingIndex, int length)
        {
            var sortRuns = new List<SortRun>();

            int currentIndex = startingIndex;
            int indexLimit = startingIndex + length;

            int elementsLeft = length;
            while (elementsLeft > 0)
            {
                var sortRun = FindNextSortRun(list, currentIndex, indexLimit);
                if (sortRun.Length < MinimalRunLength)
                {
                    sortRun = new SortRun(sortRun.Start, Math.Min(MinimalRunLength, elementsLeft));
                    GroupSortAlgorhythm.Sort(list, sortRun.Start, sortRun.Length);
                }
                sortRuns.Add(sortRun);
                elementsLeft -= sortRun.Length;
                currentIndex += sortRun.Length;
            }

            return sortRuns;
        }

        private SortRun FindNextSortRun(IList<T> list, int runStart, int indexLimit)
        {
            int runLength = 0;
            int currentIndex = runStart;

            int previousRunDirection = 0;
            var previousElement = list[runStart];

            while (currentIndex < indexLimit)
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
    }
}
