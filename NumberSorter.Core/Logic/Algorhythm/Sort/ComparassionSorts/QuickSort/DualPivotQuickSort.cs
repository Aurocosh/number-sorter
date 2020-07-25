using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.PivotSelector;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class DualPivotQuickSort<T> : GenericSortAlgorhythm<T>
    {
        private int CutoffValue { get; }
        private IPivotSelector<T> PivotSelector { get; }
        private ISortAlgorhythm<T> CutoffAlgorhythm { get; }

        public DualPivotQuickSort(IComparer<T> comparer, IPivotSelectorFactory pivotSelectorFactory, ISortFactory cutoffSortFactory, int cutoffValue) : base(comparer)
        {
            CutoffValue = cutoffValue;
            PivotSelector = pivotSelectorFactory.GetPivotSelector(comparer);
            CutoffAlgorhythm = cutoffSortFactory.GetSort(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            SortRange(list, startingIndex, startingIndex + length - 1);
        }

        private void SortRange(IList<T> list, int startingIndex, int lastIndex)
        {
            int length = lastIndex - startingIndex + 1;
            if (length < 2)
                return;

            if (length < CutoffValue)
            {
                CutoffAlgorhythm.Sort(list, startingIndex, length);
                return;
            }

            int firstPivotRangeLength = length / 2;
            int secondPivotRangeLength = length - firstPivotRangeLength;

            int firstPivotIndex = PivotSelector.SelectPivot(list, startingIndex, startingIndex + firstPivotRangeLength - 1);
            int secondPivotIndex = PivotSelector.SelectPivot(list, startingIndex + firstPivotRangeLength, lastIndex);

            list.Swap(startingIndex, firstPivotIndex);
            list.Swap(lastIndex, secondPivotIndex);

            if (Compare(list, startingIndex, lastIndex) == 1)
                list.Swap(startingIndex, lastIndex);

            T leftPivot = list[startingIndex];
            T rightPivot = list[lastIndex];

            int leftIndex = startingIndex + 1;
            int rightIndex = lastIndex - 1;
            int middleIndex = leftIndex;

            while (middleIndex <= rightIndex)
            {
                if (Compare(list[middleIndex], leftPivot) < 0)
                {
                    list.Swap(middleIndex, leftIndex);
                    leftIndex++;
                }
                else if (Compare(list[middleIndex], rightPivot) >= 0)
                {
                    while (Compare(list[rightIndex], rightPivot) > 0 && middleIndex < rightIndex)
                        rightIndex--;

                    list.Swap(middleIndex, rightIndex);
                    rightIndex--;

                    if (Compare(list[middleIndex], leftPivot) < 0)
                    {
                        list.Swap(middleIndex, leftIndex);
                        ++leftIndex;
                    }
                }
                ++middleIndex;
            }

            --leftIndex;
            ++rightIndex;

            list.Swap(startingIndex, leftIndex);
            list.Swap(lastIndex, rightIndex);

            SortRange(list, startingIndex, leftIndex - 1);
            SortRange(list, leftIndex + 1, rightIndex - 1);
            SortRange(list, rightIndex + 1, lastIndex);
        }
    }
}
