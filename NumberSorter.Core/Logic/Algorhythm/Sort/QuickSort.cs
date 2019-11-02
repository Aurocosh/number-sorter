using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.PivotSelector;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class QuickSort<T> : GenericSortAlgorhythm<T>
    {
        private int CutoffValue { get; }
        private IPivotSelector<T> PivotSelector { get; }
        private IPartialSortAlgorhythm<T> CutoffAlgorhythm { get; }

        public QuickSort(IComparer<T> comparer, IPivotSelectorFactory pivotSelectorFactory, IPartialSortFactory cutoffSortFactory, int cutoffValue) : base(comparer)
        {
            CutoffValue = cutoffValue;
            PivotSelector = pivotSelectorFactory.GetPivotSelector(comparer);
            CutoffAlgorhythm = cutoffSortFactory.GetPatrialSort(comparer);
        }

        public override void Sort(IList<T> list)
        {
            SortRange(list, 0, list.Count - 1);
        }

        private void SortRange(IList<T> list, int firstIndex, int lastIndex)
        {
            if (firstIndex >= lastIndex)
                return;

            int runRange = lastIndex - firstIndex + 1;
            if (runRange < CutoffValue)
            {
                CutoffAlgorhythm.Sort(list, firstIndex, runRange);
                return;
            }

            int pivotIndex = PivotSelector.SelectPivot(list, firstIndex, lastIndex);
            var pivot = list[pivotIndex];

            list.Swap(firstIndex, pivotIndex);
            pivotIndex = firstIndex;
            int nextBigElementIndex = lastIndex;
            int nextUnsortedIndex = pivotIndex + 1;
            int unsortedElementCount = lastIndex - firstIndex;
            while (unsortedElementCount-- > 0)
            {
                var nextUnsorted = list[nextUnsortedIndex];
                var comparrassion = Compare(pivot, nextUnsorted);
                if (comparrassion >= 0)
                    list.Swap(pivotIndex++, nextUnsortedIndex++);
                else
                    list.Swap(nextUnsortedIndex, nextBigElementIndex--);
            }
            SortRange(list, firstIndex, pivotIndex - 1);
            SortRange(list, pivotIndex + 1, lastIndex);
        }
    }
}
