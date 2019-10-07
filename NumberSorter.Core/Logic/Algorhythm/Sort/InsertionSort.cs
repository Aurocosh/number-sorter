using NumberSorter.Core.Logic.Container;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class InsertionSort<T> : GenericSortAlgorhythm<T>
    {
        public InsertionSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            for (int i = startingIndex + 1; i < length; i++)
            {
                var currentIndex = i;
                var testIndex = i - 1;
                var currentValue = list[currentIndex];

                while (testIndex > -1 && Compare(list[testIndex], currentValue) > 0)
                {
                    list.Swap(currentIndex, testIndex);
                    currentIndex = testIndex;
                    testIndex--;
                }
            }
        }
    }
}
