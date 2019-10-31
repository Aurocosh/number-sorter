using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public abstract class MultiMergeSortBase<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        private ISortRunLocator<T> SortRunLocator { get; }

        public MultiMergeSortBase(IComparer<T> comparer, ISortRunLocator<T> sortRunLocator) : base(comparer)
        {
            SortRunLocator = sortRunLocator;
        }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public abstract void SortRuns(IList<SortRun> list, IList<T> values, IComparer<T> comparer);

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRuns = FindSortRuns(list, startingIndex, length);
            if (sortRuns.Count < 2)
                return;

            SortRuns(sortRuns, list, GetComparer());
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
            int elementsLeft = length;
            while (elementsLeft > 0)
            {
                var sortRun = SortRunLocator.FindNextSortRun(list, currentIndex, elementsLeft);
                sortRuns.Add(sortRun);
                elementsLeft -= sortRun.Length;
                currentIndex += sortRun.Length;
            }

            return sortRuns;
        }
    }
}
