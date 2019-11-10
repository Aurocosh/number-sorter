using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class StableQuickSortFactory : QuickSortFactory
    {
        public StableQuickSortFactory(int cutoffSize, ISortFactory cutoffSortFactory, IPivotSelectorFactory pivotSelectorFactory) : base(cutoffSize, cutoffSortFactory, pivotSelectorFactory)
        {
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new StableQuickSort<T>(comparer, PivotSelectorFactory, CutoffSortFactory, CutoffValue);
        }
    }
}
