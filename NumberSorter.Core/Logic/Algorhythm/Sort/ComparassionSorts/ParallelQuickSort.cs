using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.PivotSelector;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class ParallelQuickSort<T> : GenericSortAlgorhythm<T>
    {
        private int CutoffValue { get; }
        private IPivotSelector<T> PivotSelector { get; }
        private ISortAlgorhythm<T> CutoffAlgorhythm { get; }

        public ParallelQuickSort(IComparer<T> comparer, IPivotSelectorFactory pivotSelectorFactory, ISortFactory cutoffSortFactory, int cutoffValue) : base(comparer)
        {
            CutoffValue = cutoffValue;
            PivotSelector = pivotSelectorFactory.GetPivotSelector(comparer);
            CutoffAlgorhythm = cutoffSortFactory.GetSort(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int parallelDepth = (int)Math.Ceiling(Math.Log(Environment.ProcessorCount, 2));
            SortRange(list, startingIndex, startingIndex + list.Count - 1, parallelDepth);
        }

        private void SortRange(IList<T> list, int firstIndex, int lastIndex, int parallelDepth)
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

            int leftIndex = firstIndex;
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

            if (parallelDepth > 0)
            {
                parallelDepth--;
                Parallel.Invoke(
                    () => SortRange(list, firstIndex, rightIndex, parallelDepth),
                    () => SortRange(list, leftIndex, lastIndex, parallelDepth));
            }
            else
            {
                SortRange(list, firstIndex, rightIndex, 0);
                SortRange(list, leftIndex, lastIndex, 0);
            }
        }
    }
}
