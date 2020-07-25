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
        private ISortAlgorhythm<T> CutoffAlgorhythm { get; }

        public QuickSort(IComparer<T> comparer, IPivotSelectorFactory pivotSelectorFactory, ISortFactory cutoffSortFactory, int cutoffValue) : base(comparer)
        {
            CutoffValue = cutoffValue;
            PivotSelector = pivotSelectorFactory.GetPivotSelector(comparer);
            CutoffAlgorhythm = cutoffSortFactory.GetSort(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            SortRange(list, startingIndex, startingIndex + list.Count - 1);
        }

        private void SortRange(IList<T> list, int startingIndex, int lastIndex)
        {
            if (startingIndex >= lastIndex)
                return;

            int runRange = lastIndex - startingIndex + 1;
            if (runRange < CutoffValue)
            {
                CutoffAlgorhythm.Sort(list, startingIndex, runRange);
                return;
            }

            int pivotIndex = PivotSelector.SelectPivot(list, startingIndex, lastIndex);
            var pivot = list[pivotIndex];

            int leftIndex = startingIndex;
            int rightIndex = lastIndex;

            while (leftIndex <= rightIndex)
            {
                while (Compare(list[leftIndex], pivot) < 0)
                    leftIndex++;
                while (Compare(list[rightIndex], pivot) > 0)
                    rightIndex--;
                if (leftIndex <= rightIndex)
                    list.Swap(leftIndex++, rightIndex--);
            }

            if (startingIndex < rightIndex)
                SortRange(list, startingIndex, rightIndex);
            if (leftIndex < lastIndex)
                SortRange(list, leftIndex, lastIndex);
        }
    }
}
