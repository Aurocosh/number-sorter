using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class SelectionSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        public SelectionSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
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

        private int BinarySearch(IList<T> list, T elementToInsert, int low, int high)
        {
            if (high <= low)
                return (Compare(list[low], elementToInsert) > 0) ? low : low + 1;

            int index = (low + high) / 2;
            int comparassion = Compare(elementToInsert, list[index]);
            if (comparassion == 0)
                return index + 1;
            else if (comparassion > 0)
                return BinarySearch(list, elementToInsert, index + 1, high);
            else
                return BinarySearch(list, elementToInsert, low, index - 1);
        }
    }
}
