using NumberSorter.Domain.Logic.Container;
using System.Collections.Generic;

namespace NumberSorter.Domain.Logic.Algorhythm
{
    public class InsertionSort<T> : GenericSortAlgorhythm<T>
    {
        public InsertionSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            int count = list.Count;

            for (int i = 1; i < count; i++)
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
