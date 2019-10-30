using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class MultiMergeSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        private Func<IComparer<SortRun>, IPartialSortAlgorhythm<SortRun>> RunSortFactory { get; }

        public MultiMergeSort(IComparer<T> comparer, Func<IComparer<SortRun>, IPartialSortAlgorhythm<SortRun>> runSortFactory) : base(comparer)
        {
            RunSortFactory = runSortFactory;
        }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRuns = FindSortRuns(list, startingIndex, length);
            var comparer = new FirstRunElementComparer<T>(list, GetComparer());
            var runSorter = RunSortFactory.Invoke(comparer);

            runSorter.Sort(sortRuns);

            var temporartArray = list.GetRangeAsArray(startingIndex, length);

            int currentRunIndex = 0;
            int runIndexLimit = sortRuns.Count;

            int index = startingIndex;
            int indexLimit = startingIndex + length;

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

            int runStart = startingIndex;
            int runLength = 0;

            int currentIndex = startingIndex;
            int indexLimit = startingIndex + length;

            int previousRunDirection = 0;
            var previousElement = list[startingIndex];

            while (currentIndex != indexLimit)
            {
                var nextElement = list[currentIndex];
                var runDirection = Math.Sign(Compare(previousElement, nextElement));
                if (previousRunDirection != 0 && previousRunDirection != runDirection)
                {
                    sortRuns.Add(GetSortRun(list, runStart, runLength, previousRunDirection));
                    previousRunDirection = 0;
                    runStart = currentIndex;
                    runLength = 1;
                }
                else
                {
                    previousRunDirection = runDirection;
                    runLength++;
                }

                previousElement = nextElement;
                currentIndex++;
            }

            sortRuns.Add(GetSortRun(list, runStart, runLength, previousRunDirection));
            return sortRuns;
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
