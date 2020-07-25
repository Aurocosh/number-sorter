using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.PivotSelector;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class StableQuickSort<T> : GenericSortAlgorhythm<T>
    {
        private int CutoffValue { get; }
        private IPivotSelector<T> PivotSelector { get; }
        private ISortAlgorhythm<T> CutoffAlgorhythm { get; }

        public StableQuickSort(IComparer<T> comparer, IPivotSelectorFactory pivotSelectorFactory, ISortFactory cutoffSortFactory, int cutoffValue) : base(comparer)
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
            if (startingIndex >= lastIndex)
                return;

            int runRange = lastIndex - startingIndex + 1;
            if (runRange < CutoffValue)
            {
                CutoffAlgorhythm.Sort(list, startingIndex, runRange);
                return;
            }

            var buffer = new T[runRange];

            int pivotIndex = PivotSelector.SelectPivot(list, startingIndex, lastIndex);
            var pivot = list[pivotIndex];

            list.Swap(startingIndex, pivotIndex);

            int bufferIndex = 0;
            int firstIndex = startingIndex + 1;

            int indexLimit = lastIndex + 1;
            for (int sourceIndex = firstIndex; sourceIndex < indexLimit; sourceIndex++)
            {
                var value = list[sourceIndex];
                if (Compare(value, pivot) <= 0)
                    list[firstIndex++] = value;
                else
                    buffer[bufferIndex++] = value;
            }

            int rightLength = bufferIndex;
            ListUtility.Copy(buffer, 0, list, firstIndex, rightLength);

            pivotIndex = firstIndex - 1;
            list.Swap(startingIndex, pivotIndex);

            int lastLeftIndex = pivotIndex - 1;
            if (startingIndex < lastLeftIndex)
                SortRange(list, startingIndex, lastLeftIndex);
            if (firstIndex < lastIndex)
                SortRange(list, firstIndex, lastIndex);
        }
    }
}
