using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class IntervalMultiMergeSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        private ISortFactory RunSortFactory { get; }
        private ISortRunLocator<T> SortRunLocator { get; }
        private IPositionLocatorFactory PositionLocatorFactory { get; }

        public IntervalMultiMergeSort(IComparer<T> comparer, ISortFactory runSortFactory, ISortRunLocatorFactory sortRunLocatorFactory, IPositionLocatorFactory positionLocatorFactory) : base(comparer)
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

            int lowRunIndex = 0;
            int runIndexLimit = sortRuns.Count;
            int lastRunIndex = sortRuns.Count - 1;

            SortRun nextSortRun = sortRuns[lowRunIndex];

            int index = startingIndex;
            var elementPositionLocator = PositionLocatorFactory.GetPositionLocator(Comparer);
            var sortRunPositionLocator = PositionLocatorFactory.GetPositionLocator(sortRunComparer);

            while (lowRunIndex != lastRunIndex)
            {
                SortRun lowSortRun = nextSortRun;

                int nextRunIndex = lowRunIndex + 1;
                nextSortRun = sortRuns[nextRunIndex];
                T nextLimit = list[nextSortRun.FirstIndex];

                int start = lowSortRun.FirstIndex;
                int end = elementPositionLocator.FindLastPosition(list, nextLimit, lowSortRun.FirstIndex, lowSortRun.Length);

                int elementsToCopy = end - start + 1;
                ListUtility.Copy(list, start, temporaryArray, index, elementsToCopy);
                index += elementsToCopy;

                var newRun = new SortRun(lowSortRun.FirstIndex + elementsToCopy, lowSortRun.Length - elementsToCopy);
                sortRuns[lowRunIndex] = newRun;

                if (newRun.Length == 0)
                {
                    lowRunIndex++;
                    continue;
                }

                int searchAreaLength = runIndexLimit - nextRunIndex;
                int indexToInsert = sortRunPositionLocator.FindFirstPosition(sortRuns, newRun, nextRunIndex, searchAreaLength);

                int newRunIndex = lowRunIndex;
                while (newRunIndex != indexToInsert)
                {
                    sortRuns.Swap(newRunIndex, newRunIndex + 1);
                    newRunIndex++;
                }
            }

            ListUtility.Copy(list, nextSortRun.FirstIndex, temporaryArray, index, nextSortRun.Length);
            ListUtility.Copy(temporaryArray, 0, list, 0, length);
        }

        private int FindEndOfRun(IList<T> list, SortRun sortRun, T upperLimit)
        {
            int index = sortRun.FirstIndex;
            int indexLimit = index + sortRun.Length;

            while (index != indexLimit && Compare(upperLimit, list[index]) >= 0)
                index++;
            return index - 1;
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
