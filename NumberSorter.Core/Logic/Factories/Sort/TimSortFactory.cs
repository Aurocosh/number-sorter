using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class TimSortFactory : GenericSortFactory
    {
        private IRunMergerFactory MergerFactory { get; }
        private ISortFactory MinrunSortFactory { get; }

        public TimSortFactory(IRunMergerFactory mergerFactory, ISortFactory minrunSortFactory)
        {
            MergerFactory = mergerFactory;
            MinrunSortFactory = minrunSortFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new TimSort<T>(comparer, MergerFactory, MinrunSortFactory);
        }
    }
}
