using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class MultiMergeSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        private ISortFactory RunSortFactory { get; }
        private ISortRunLocator<T> SortRunLocator { get; }

        public MultiMergeSort(IComparer<T> comparer, ISortFactory runSortFactory, ISortRunLocatorFactory sortRunLocatorFactory) : base(comparer)
        {
            RunSortFactory = runSortFactory;
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

            var compar = Comparer<SortRun>.Create((x, y) =>
                  {
                      var first = list[x.FirstIndex];
                      var second = list[y.FirstIndex];
                      return Compare(first, second);
                  }
               );
            RunSortFactory.Sort(sortRuns, compar);

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
