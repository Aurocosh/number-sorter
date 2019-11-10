using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class SelectionSort<T> : GenericSortAlgorhythm<T>
    {
        public SelectionSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            if (length <= 1)
                return;

            int lowerLimitIndex = startingIndex;
            int upperLimitIndex = startingIndex + length;
            while (lowerLimitIndex != upperLimitIndex)
            {
                int lowestIndex = lowerLimitIndex;
                int currentIndex = lowerLimitIndex;
                T currentMinimum = list[lowerLimitIndex];
                while (currentIndex < upperLimitIndex)
                {
                    T currentValue = list[currentIndex];
                    if (Compare(currentMinimum, currentValue) > 0)
                    {
                        lowestIndex = currentIndex;
                        currentMinimum = currentValue;
                    }
                    currentIndex++;
                }
                if (lowestIndex != lowerLimitIndex)
                    list.Swap(lowerLimitIndex, lowestIndex);
                lowerLimitIndex++;
            }
        }
    }
}
