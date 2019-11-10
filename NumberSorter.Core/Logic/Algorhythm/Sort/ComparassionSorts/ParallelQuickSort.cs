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
            int parallelDepth = Environment.ProcessorCount;
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
            if (parallelDepth > 0)
            {
                parallelDepth--;
                Parallel.Invoke(
                    () => SortRange(list, firstIndex, pivotIndex - 1, parallelDepth),
                    () => SortRange(list, pivotIndex + 1, lastIndex, parallelDepth));
            }
            else
            {
                SortRange(list, firstIndex, pivotIndex - 1, 0);
                SortRange(list, pivotIndex + 1, lastIndex, 0);
            }
        }
    }
}
