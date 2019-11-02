using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class BinarySort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        public BinarySort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            int indexLimit = startingIndex + length;
            for (int index = startingIndex + 1; index != indexLimit; index++)
            {
                var currentValue = list[index];
                int indexToInsert = BinarySearch(list, currentValue, startingIndex, index - 1);

                int currentIndex = index;
                while (currentIndex != indexToInsert)
                {
                    list.Swap(currentIndex - 1, currentIndex);
                    currentIndex--;
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
