using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class QuickSortFactory : GenericSortFactory
    {
        protected int CutoffValue { get; }
        protected ISortFactory CutoffSortFactory { get; }
        protected IPivotSelectorFactory PivotSelectorFactory { get; }

        public QuickSortFactory(int cutoffSize, ISortFactory cutoffSortFactory, IPivotSelectorFactory pivotSelectorFactory)
        {
            CutoffValue = cutoffSize;
            CutoffSortFactory = cutoffSortFactory;
            PivotSelectorFactory = pivotSelectorFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new QuickSort<T>(comparer, PivotSelectorFactory, CutoffSortFactory, CutoffValue);
        }
    }
}
