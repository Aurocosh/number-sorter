using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class RecursiveMultiMergeSortFactory : GenericSortFactory
    {
        private ISortRunLocatorFactory SortRunLocatorFactory { get; }
        private IPositionLocatorFactory PositionLocatorFactory { get; }

        public RecursiveMultiMergeSortFactory(ISortRunLocatorFactory sortRunLocatorFactory, IPositionLocatorFactory positionLocatorFactory)
        {
            SortRunLocatorFactory = sortRunLocatorFactory;
            PositionLocatorFactory = positionLocatorFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new MultiMergeSort<T>(comparer, this, SortRunLocatorFactory, PositionLocatorFactory);
        }
    }
}
