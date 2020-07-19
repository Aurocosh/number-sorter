using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class RecursiveMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeAlgothythm<T> LocalMergeAlgorhythm { get; }

        public RecursiveMergeSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            LocalMergeAlgorhythm = localMergeFactory.GetLocalMerge(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRun = new SortRun(startingIndex, length);
            MergeSort(list, sortRun);
        }

        private void MergeSort(IList<T> list, SortRun sortRun)
        {
            if (sortRun.Length <= 1)
                return;

            var halvesOfSortRun = SortRunUtility.SplitSortRun(sortRun);
            MergeSort(list, halvesOfSortRun.First);
            MergeSort(list, halvesOfSortRun.Second);

            LocalMergeAlgorhythm.Merge(list, halvesOfSortRun.First, halvesOfSortRun.Second);
        }
    }
}
