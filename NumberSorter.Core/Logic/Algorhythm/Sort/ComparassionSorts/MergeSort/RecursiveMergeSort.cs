using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class RecursiveMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private readonly ILocalMergeFactory _localMergeFactory;
        private ILocalMergeAlgothythm<T> _localMergeAlgothythm;

        public RecursiveMergeSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            _localMergeFactory = localMergeFactory;
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRun = new SortRun(startingIndex, length);
            _localMergeAlgothythm = _localMergeFactory.GetLocalMerge(Comparer, list);
            MergeSort(list, sortRun);
        }

        private void MergeSort(IList<T> list, SortRun sortRun)
        {
            if (sortRun.Length <= 1)
                return;

            var halvesOfSortRun = SortRunUtility.SplitSortRun(sortRun);
            MergeSort(list, halvesOfSortRun.First);
            MergeSort(list, halvesOfSortRun.Second);

            _localMergeAlgothythm.Merge(list, halvesOfSortRun.First, halvesOfSortRun.Second);
        }
    }
}
