using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort.Base
{
    public interface ISortFactory
    {
        ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer);
        void Sort<T>(IList<T> list, IComparer<T> comparer);
    }
}
