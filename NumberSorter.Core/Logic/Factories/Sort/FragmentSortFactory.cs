using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class FragmentSortFactory : GenericSortFactory
    {
        protected int FragmentSize { get; }
        protected ISortFactory SortFactory { get; }

        public FragmentSortFactory(int fragmentSize, ISortFactory sortFactory)
        {
            FragmentSize = fragmentSize;
            SortFactory = sortFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new FragmentSort<T>(comparer, SortFactory, FragmentSize);
        }
    }
}
