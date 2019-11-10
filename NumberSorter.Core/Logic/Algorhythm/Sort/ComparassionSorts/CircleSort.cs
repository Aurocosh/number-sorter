using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class CircleSort<T> : GenericSortAlgorhythm<T>
    {
        public CircleSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int firstIndex, int length)
        {
            while (!TryToSort(list, firstIndex, length)) ;
        }

        public bool TryToSort(IList<T> list, int firstIndex, int length)
        {
            int lastIndex = firstIndex + length - 1;
            if (firstIndex == lastIndex)
                return true;

            bool sorted = true;
            int highIndex = lastIndex;
            int lowIndex = firstIndex;
            while (lowIndex < highIndex)
            {
                if (Compare(list, lowIndex, highIndex) > 0)
                {
                    list.Swap(lowIndex, highIndex);
                    sorted = false;
                }
                lowIndex++;
                highIndex--;
            }
            if (lowIndex == highIndex && Compare(list, lowIndex - 1, highIndex) > 0)
            {
                list.Swap(lowIndex - 1, highIndex);
                sorted = false;
            }
            int leftLength = length / 2;
            int rightLength = length - leftLength;

            bool leftSorted = TryToSort(list, firstIndex, leftLength);
            bool rightSorted = TryToSort(list, firstIndex + leftLength, rightLength);
            return sorted && leftSorted && rightSorted;
        }
    }
}
