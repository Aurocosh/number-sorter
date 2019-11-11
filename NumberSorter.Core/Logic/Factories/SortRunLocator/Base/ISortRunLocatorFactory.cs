using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge.Base
{
    public interface ISortRunLocatorFactory
    {
        ISortRunLocator<T> GetSortRunLocator<T>(IComparer<T> comparer);
    }
}
