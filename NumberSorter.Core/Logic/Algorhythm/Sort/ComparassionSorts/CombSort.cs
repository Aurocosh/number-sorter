using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class CombSort<T> : GenericSortAlgorhythm<T>
    {
        public CombSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int currentGap = length;
            const float gapDecreaseFactor = 1.3f;
            int lastIndexLimit = startingIndex + length;
            do
            {
                currentGap = (int)Math.Max(Math.Floor(currentGap / gapDecreaseFactor), 1);
                int firstIndex = startingIndex;
                int secondIndex = startingIndex + currentGap;
                Comb(list, firstIndex, secondIndex, lastIndexLimit);
            } while (currentGap != 1);

            int finalSecondIndex = startingIndex + 1;
            while (!Comb(list, startingIndex, finalSecondIndex, lastIndexLimit)) ;
        }

        private bool Comb(IList<T> list, int firstIndex, int secondIndex, int lastIndexLimit)
        {
            bool sorted = true;
            while (secondIndex < lastIndexLimit)
            {
                T firstValue = list[firstIndex];
                T secondValue = list[secondIndex];
                if (Compare(firstValue, secondValue) > 0)
                {
                    sorted = false;
                    list[firstIndex] = secondValue;
                    list[secondIndex] = firstValue;
                }

                firstIndex++;
                secondIndex++;
            }
            return sorted;
        }
    }
}
