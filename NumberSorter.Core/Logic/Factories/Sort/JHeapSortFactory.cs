using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class JHeapSortFactory : GenericSortFactory
    {
        private ISortFactory FinalSortFactory { get; }

        public JHeapSortFactory(ISortFactory finalSortFactory)
        {
            FinalSortFactory = finalSortFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new JHeapSort<T>(comparer, FinalSortFactory);
        }
    }
}
