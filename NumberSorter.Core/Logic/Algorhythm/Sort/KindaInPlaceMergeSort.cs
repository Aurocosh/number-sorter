using NumberSorter.Core.Logic.Container;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class KindaInPlaceMergeSort<T> : GenericSortAlgorhythm<T>
    {
        public KindaInPlaceMergeSort(IComparer<T> comparer) : base(comparer)
        {
        }

        public override void Sort(IList<T> list)
        {
            var sortRun = new SortRun(0, list.Count);
            MergeSort(list, sortRun);
        }

        private void MergeSort(IList<T> list, SortRun sortRun)
        {
            if (sortRun.Length <= 1)
                return;

            var halvesOfSortRun = SortRunUtility.SplitSortRun(sortRun);
            MergeSort(list, halvesOfSortRun.First);
            MergeSort(list, halvesOfSortRun.Second);

            Merge(list, halvesOfSortRun.First, halvesOfSortRun.Second);
        }

        private void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length + secondRun.Length < 2)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            int unsortedInFirst = firstRun.Length;
            int unsortedInSecond = secondRun.Length;

            while (Compare(list, firstIndex, secondIndex) <= 0 && unsortedInFirst > 0)
            {
                unsortedInFirst--;
                firstIndex++;
            }

            int temporaryIndex = 0;
            var temporartArray = list.GetRangeAsArray(firstIndex, unsortedInFirst);

            while (unsortedInFirst > 0 && unsortedInSecond > 0)
            {
                if (Compare(temporartArray[temporaryIndex], list[secondIndex]) > 0)
                {
                    list[firstIndex++] = list[secondIndex++];
                    unsortedInSecond--;
                }
                else
                {
                    list[firstIndex++] = temporartArray[temporaryIndex++];
                    unsortedInFirst--;
                }
            }

            while (unsortedInFirst-- > 0)
                list[firstIndex++] = temporartArray[temporaryIndex++];

            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }
    }
}
