using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class MultiMergeSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
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

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRuns = FindSortRuns(list, startingIndex, length);
            if (sortRuns.Count < 2)
                return;

            var temporaryArray = new T[length];

            var sortRunComparer = Comparer<SortRun>.Create((x, y) => Compare(list[x.FirstIndex], list[y.FirstIndex]));
            RunSortFactory.Sort(sortRuns, sortRunComparer);

            int currentRunIndex = 0;
            int runIndexLimit = sortRuns.Count;

            int index = startingIndex;
            var positionLocator = PositionLocatorFactory.GetPositionLocator(sortRunComparer);

            while (currentRunIndex != runIndexLimit)
            {
                var lowestRun = sortRuns[currentRunIndex];
                temporaryArray[index++] = list[lowestRun.FirstIndex];

                var newRun = new SortRun(lowestRun.FirstIndex + 1, lowestRun.Length - 1);
                sortRuns[currentRunIndex] = newRun;

                if (newRun.Length == 0)
                {
                    currentRunIndex++;
                    continue;
                }

                int nextRunIndex = currentRunIndex + 1;
                if (nextRunIndex != runIndexLimit)
                {
                    var nextRun = sortRuns[nextRunIndex];
                    if (sortRunComparer.Compare(newRun, nextRun) > 0)
                    {
                        int searchAreaLength = runIndexLimit - nextRunIndex;
                        int indexToInsert = positionLocator.FindFirstPosition(sortRuns, newRun, nextRunIndex, searchAreaLength);

                        int newRunIndex = currentRunIndex;
                        while (newRunIndex != indexToInsert)
                        {
                            sortRuns.Swap(newRunIndex, newRunIndex + 1);
                            newRunIndex++;
                        }
                    }
                }
            }
            ListUtility.Copy(temporaryArray, 0, list, 0, length);
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
