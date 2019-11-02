using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class MultiMergeSortFactory : GenericSortFactory
    {
        private ISortFactory RunSortFactory { get; }
        private ISortRunLocatorFactory SortRunLocatorFactory { get; }

        public MultiMergeSortFactory(ISortFactory runSortFactory, ISortRunLocatorFactory sortRunLocatorFactory)
        {
            RunSortFactory = runSortFactory;
            SortRunLocatorFactory = sortRunLocatorFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new MultiMergeSort<T>(comparer, RunSortFactory, SortRunLocatorFactory);
        }
    }
}
