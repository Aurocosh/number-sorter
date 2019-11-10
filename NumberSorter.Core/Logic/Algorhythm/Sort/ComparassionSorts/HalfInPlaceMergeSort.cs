using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class HalfInPlaceMergeSort<T> : GenericSortAlgorhythm<T>
    {
        public HalfInPlaceMergeSort(IComparer<T> comparer) : base(comparer)
        {
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var sortRun = new SortRun(startingIndex, length);
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
