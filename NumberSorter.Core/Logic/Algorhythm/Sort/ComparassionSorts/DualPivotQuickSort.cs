using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class DualPivotQuickSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        private int CutoffValue { get; }
        private IPartialSortAlgorhythm<T> CutoffAlgorhythm { get; }

        public DualPivotQuickSort(IComparer<T> comparer, IPartialSortFactory cutoffSortFactory, int cutoffValue) : base(comparer)
        {
            CutoffValue = cutoffValue;
            CutoffAlgorhythm = cutoffSortFactory.GetPatrialSort(comparer);
        }

        public override void Sort(IList<T> list)
        {
            SortRange(list, 0, list.Count - 1);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            SortRange(list, startingIndex, startingIndex + list.Count - 1);
        }

        private void SortRange(IList<T> list, int startingIndex, int lastIndex)
        {
            if (lastIndex <= startingIndex)
                return;

            int runRange = lastIndex - startingIndex + 1;
            if (runRange < CutoffValue)
            {
                CutoffAlgorhythm.Sort(list, startingIndex, runRange);
                return;
            }

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
