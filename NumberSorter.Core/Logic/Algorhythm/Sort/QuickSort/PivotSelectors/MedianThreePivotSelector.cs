using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.QuickSort.PivotSelectors
{
    public sealed class MedianThreePivotSelector<T> : QuickSortPivotSelector<T>
    {
        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex, IComparer<T> comparer)
        {
            int centerIndex = (firstIndex + lastIndex) / 2;

            if (comparer.Compare(list[centerIndex], list[firstIndex]) < 0)
                list.Swap(firstIndex, centerIndex);
            if (comparer.Compare(list[lastIndex], list[firstIndex]) < 0)
                list.Swap(firstIndex, lastIndex);
            if (comparer.Compare(list[lastIndex], list[centerIndex]) < 0)
                list.Swap(centerIndex, lastIndex);
            return centerIndex;
        }
    }
}
