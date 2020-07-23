using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class DualStrandSortFactory : GenericSortFactory
    {
        private IRunMergerFactory MergerFactory { get; }

        public DualStrandSortFactory(IRunMergerFactory mergerFactory)
        {
            MergerFactory = mergerFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new DualStrandSort<T>(comparer, MergerFactory);
        }
    }
}
