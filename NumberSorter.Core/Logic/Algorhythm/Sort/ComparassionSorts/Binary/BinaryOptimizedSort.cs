using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class BinaryOptimizedSort<T> : GenericSortAlgorhythm<T>
    {
        public BinaryOptimizedSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            if (length == 0)
                return;

            int indexLimit = startingIndex + length;
            for (int index = startingIndex + 1; index != indexLimit; index++)
            {
                var currentValue = list[index];
                int indexToInsert = BinarySearch(list, currentValue, startingIndex, index - 1);

                if (index != indexToInsert)
                {
                    int targetIndex = index;
                    int sourceIndex = targetIndex - 1;

                    do
                        list[targetIndex--] = list[sourceIndex--];
                    while (targetIndex != indexToInsert);

                    list[indexToInsert] = currentValue;
                }
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
