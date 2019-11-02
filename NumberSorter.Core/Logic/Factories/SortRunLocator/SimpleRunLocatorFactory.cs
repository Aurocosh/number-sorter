using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SortRunLocator;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.SortRunLocator
{
    public class SimpleRunLocatorFactory : ISortRunLocatorFactory
    {
        public ISortRunLocator<T> GetSortRunLocator<T>(IComparer<T> comparer)
        {
            return new SimpleRunLocator<T>(comparer);
        }
    }
}
