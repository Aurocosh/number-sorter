using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class PresortBottomUpMergeSortFactory : GenericSortFactory
    {
        private ISortFactory SmallRunSortFactory { get; }
        private ILocalMergeFactory LocalMergeFactory { get; }

        public PresortBottomUpMergeSortFactory(ISortFactory smallRunSortFactory, ILocalMergeFactory localMergeFactory)
        {
            SmallRunSortFactory = smallRunSortFactory;
            LocalMergeFactory = localMergeFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new PresortBottomUpMergeSort<T>(comparer, SmallRunSortFactory, LocalMergeFactory);
        }
    }
}
