using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class RecursiveMergeSortFactory : GenericSortFactory
    {
        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new RecursiveMergeSort<T>(comparer);
        }
    }
}
