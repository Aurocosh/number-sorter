using NumberSorter.Algorhythm;
using NumberSorter.Logic.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm
{
    public class InsertionSort<T> : ComparerSort<T>
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
