using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.PivotSelector;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class QuickSortLL<T> : GenericSortAlgorhythm<T>
    {
        private int CutoffValue { get; }
        private IPivotSelector<T> PivotSelector { get; }
        private ISortAlgorhythm<T> CutoffAlgorhythm { get; }

        public QuickSortLL(IComparer<T> comparer, IPivotSelectorFactory pivotSelectorFactory, ISortFactory cutoffSortFactory, int cutoffValue) : base(comparer)
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
            int runRange = lastIndex - startingIndex + 1;
            if (runRange < CutoffValue)
            {
                CutoffAlgorhythm.Sort(list, startingIndex, runRange);
                return;
            }

            if (startingIndex < lastIndex)
            {
                int partitionIndex = Partition(list, startingIndex, lastIndex);
                SortRange(list, startingIndex, partitionIndex - 1);
                SortRange(list, partitionIndex + 1, lastIndex);
            }
        }

        private int Partition(IList<T> list, int startingIndex, int lastIndex)
        {
            int pivotIndex = PivotSelector.SelectPivot(list, startingIndex, lastIndex);
            T pivot = list[pivotIndex];
            list.Swap(pivotIndex, lastIndex);

            int partitionIndex = startingIndex;
            for (int index = startingIndex; index < lastIndex; index++)
            {
                if (Compare(list[index], pivot) < 0)
                {
                    list.Swap(partitionIndex, index);
                    partitionIndex++;
                }
            }
            list.Swap(partitionIndex, lastIndex);
            return partitionIndex;
        }
    }
}
