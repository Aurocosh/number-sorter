using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class MultiMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private ISortFactory RunSortFactory { get; }
        private ISortRunLocator<T> SortRunLocator { get; }
        private IPositionLocatorFactory PositionLocatorFactory { get; }

        public MultiMergeSort(IComparer<T> comparer, ISortFactory runSortFactory, ISortRunLocatorFactory sortRunLocatorFactory, IPositionLocatorFactory positionLocatorFactory) : base(comparer)
        {
            RunSortFactory = runSortFactory;
            PositionLocatorFactory = positionLocatorFactory;
            SortRunLocator = sortRunLocatorFactory.GetSortRunLocator(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRuns = FindSortRuns(list, startingIndex, length);
            if (sortRuns.Count < 2)
                return;

            var buffer = new T[length];

            var sortRunComparer = Comparer<SortRun>.Create((x, y) => Compare(list[x.FirstIndex], list[y.FirstIndex]));
            RunSortFactory.Sort(sortRuns, sortRunComparer);

            int lowRunIndex = 0;
            int runIndexLimit = sortRuns.Count;
            int lastRunIndex = sortRuns.Count - 1;

            int bufferIndex = startingIndex;
            var positionLocator = PositionLocatorFactory.GetPositionLocator(sortRunComparer);

            while (lowRunIndex != lastRunIndex)
            {
                //var firsts = sortRuns.Select(x => list[x.FirstIndex].ToString()).ToList();
                //var firStr = string.Join(",", firsts);

                var lowestRun = sortRuns[lowRunIndex];

                int nextRunIndex = lowRunIndex + 1;
                var nextRun = sortRuns[nextRunIndex];
                var nextRunValue = list[nextRun.FirstIndex];

                int currentRunValueIndex = lowestRun.FirstIndex;
                int currentRunIndexLimit = lowestRun.FirstIndex + lowestRun.Length;
                do
                    buffer[bufferIndex++] = list[currentRunValueIndex++];
                while (currentRunValueIndex < currentRunIndexLimit && Compare(list[currentRunValueIndex], nextRunValue) <= 0);

                int newRunLength = currentRunIndexLimit - currentRunValueIndex;
                var newRun = new SortRun(currentRunValueIndex, newRunLength);
                sortRuns[lowRunIndex] = newRun;

                if (newRun.Length == 0)
                {
                    lowRunIndex++;
                    continue;
                }

                //var firsts2 = sortRuns.Select(x => list[x.FirstIndex].ToString()).ToList();
                //var firStr2 = string.Join(",", firsts2);

                int searchAreaLength = runIndexLimit - nextRunIndex - 1;
                int newIndexOfCurrent = positionLocator.FindLastPosition(sortRuns, newRun, nextRunIndex + 1, searchAreaLength) - 1;

                var plannedRun = sortRuns[lowRunIndex];

                int targetIndex = lowRunIndex;
                int sourceIndex = targetIndex + 1;
                do
                    sortRuns[targetIndex++] = sortRuns[sourceIndex++];
                while (targetIndex < newIndexOfCurrent);

                sortRuns[newIndexOfCurrent] = plannedRun;
            }

            var lastSortRun = sortRuns[lastRunIndex];
            ListUtility.Copy(list, lastSortRun.FirstIndex, buffer, bufferIndex, lastSortRun.Length);
            ListUtility.Copy(buffer, 0, list, 0, length);
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
