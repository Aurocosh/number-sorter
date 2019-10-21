using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class DoubleSelectionSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        public DoubleSelectionSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            if (length <= 1)
                return;

            int lowerLimitIndex = startingIndex;
            int upperLimitIndex = startingIndex + length - 1;
            while (lowerLimitIndex <= upperLimitIndex)
            {
                int minimumIndex = lowerLimitIndex;
                int maximumIndex = upperLimitIndex;
                int currentIndex = lowerLimitIndex;

                T currentMinimum = list[lowerLimitIndex];
                T currentMaximum = list[upperLimitIndex];
                while (currentIndex <= upperLimitIndex)
                {
                    T currentValue = list[currentIndex];
                    if (Compare(currentMinimum, currentValue) > 0)
                    {
                        minimumIndex = currentIndex;
                        currentMinimum = currentValue;
                    }
                    if (Compare(currentMaximum, currentValue) < 0)
                    {
                        maximumIndex = currentIndex;
                        currentMaximum = currentValue;
                    }
                    currentIndex++;
                }
                if (maximumIndex == lowerLimitIndex)
                    maximumIndex = minimumIndex;
                if (minimumIndex != lowerLimitIndex)
                    list.Swap(lowerLimitIndex, minimumIndex);
                if (maximumIndex != upperLimitIndex)
                    list.Swap(upperLimitIndex, maximumIndex);
                lowerLimitIndex++;
                upperLimitIndex--;
            }
        }
    }
}
