using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class StrandMergeSortFactory : GenericSortFactory
    {
        private ILocalMergeFactory LocalMergeFactory { get; }

        public StrandMergeSortFactory(ILocalMergeFactory localMergeFactory)
        {
            LocalMergeFactory = localMergeFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new StrandSort<T>(comparer, LocalMergeFactory);
        }
    }
}
