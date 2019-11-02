using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public abstract class QuickSortFactory : GenericSortFactory
    {
        private int CutoffValue { get; }
        private IPartialSortFactory CutoffSortFactory { get; }
        private IPivotSelectorFactory PivotSelectorFactory { get; }

        protected QuickSortFactory(int cutoffSize, IPartialSortFactory cutoffSortFactory, IPivotSelectorFactory pivotSelectorFactory)
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
