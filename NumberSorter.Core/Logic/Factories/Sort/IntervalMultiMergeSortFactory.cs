using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class IntervalMultiMergeSortFactory : GenericSortFactory
    {
        private ISortFactory RunSortFactory { get; }
        private ISortRunLocatorFactory SortRunLocatorFactory { get; }
        private IPositionLocatorFactory PositionLocatorFactory { get; }

        public IntervalMultiMergeSortFactory(ISortFactory runSortFactory, ISortRunLocatorFactory sortRunLocatorFactory, IPositionLocatorFactory positionLocatorFactory)
        {
            RunSortFactory = runSortFactory;
            SortRunLocatorFactory = sortRunLocatorFactory;
            PositionLocatorFactory = positionLocatorFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new IntervalMultiMergeSort<T>(comparer, RunSortFactory, SortRunLocatorFactory, PositionLocatorFactory);
        }
    }
}
