using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class StrandMergerSortFactory : GenericSortFactory
    {
        private IRunMergerFactory MergerFactory { get; }

        public StrandMergerSortFactory(IRunMergerFactory mergerFactory)
        {
            MergerFactory = mergerFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new StrandSortMerger<T>(comparer, MergerFactory);
        }
    }
}
