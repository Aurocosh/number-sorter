using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class InsertionSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        public InsertionSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            int lowerLimit = startingIndex - 1;
            int upperLimit = startingIndex + length;
            for (int i = startingIndex + 1; i < upperLimit; i++)
            {
                var currentIndex = i;
                var testIndex = i - 1;
                var currentValue = list[currentIndex];

                while (testIndex > lowerLimit && Compare(list[testIndex], currentValue) > 0)
                {
                    list.Swap(currentIndex, testIndex);
                    currentIndex = testIndex;
                    testIndex--;
                }
            }
        }
    }
}
