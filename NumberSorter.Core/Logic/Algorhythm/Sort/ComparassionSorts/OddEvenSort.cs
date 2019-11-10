using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class OddEvenSort<T> : GenericSortAlgorhythm<T>
    {
        public OddEvenSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            if (length <= 1)
                return;

            int firstOddIndex = startingIndex;
            int secondOddIndex = startingIndex + 1;

            int firstEvenIndex = startingIndex + 1;
            int secondEvenIndex = startingIndex + 2;

            bool oddSorted = false;
            bool evenSorted = false;
            int lastIndex = startingIndex + length;
            while (!oddSorted || !evenSorted)
            {
                oddSorted = SortPairs(list, firstOddIndex, secondOddIndex, lastIndex);
                evenSorted = SortPairs(list, firstEvenIndex, secondEvenIndex, lastIndex);
            }
        }

        private bool SortPairs(IList<T> list, int firstIndex, int secondIndex, int lastIndex)
        {
            bool sorted = true;
            while (secondIndex < lastIndex)
            {
                T firstValue = list[firstIndex];
                T secondValue = list[secondIndex];
                if (Compare(firstValue, secondValue) > 0)
                {
                    sorted = false;
                    list[firstIndex] = secondValue;
                    list[secondIndex] = firstValue;
                }
                firstIndex += 2;
                secondIndex += 2;
            }
            return sorted;
        }
    }
}
