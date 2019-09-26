using NumberSorter.Algorhythm;
using NumberSorter.Logic.Algorhythm.QuickSort;
using NumberSorter.Logic.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm
{
    public class QuickSort<T> : GenericSortAlgorhythm<T>
    {
        private readonly QuickSortPivotSelector<T> _pivotSelector;

        public QuickSort(IComparer<T> comparer, QuickSortPivotSelector<T> pivotSelector) : base(comparer)
        {
            _pivotSelector = pivotSelector;
        }

        public override void Sort(IList<T> list)
        {
            SortRange(list, 0, list.Count - 1);
        }

        private void SortRange(IList<T> list, int firstIndex, int lastIndex)
        {
            if (firstIndex >= lastIndex)
                return;

            int pivotIndex = _pivotSelector.SelectPivot(list, firstIndex, lastIndex, GetComparer());
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
