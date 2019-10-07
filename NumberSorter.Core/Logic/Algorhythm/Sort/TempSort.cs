using NumberSorter.Core.Logic.Container;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class TempSort<T> : GenericSortAlgorhythm<T>
    {
        public TempSort(IComparer<T> comparer) : base(comparer) { }

        private sealed class SortRun
        {
            public int Start { get; }
            public int Length { get; }

            public SortRun(int start, int length)
            {
                Start = start;
                Length = length;
            }
        }

        public override void Sort(IList<T> list)
        {
            if (list.Count < 2)
                return;

            var sortRuns = FindSortRuns(list);

        }

        List<SortRun> FindSortRuns(IList<T> list)
        {
            var sortRuns = new List<SortRun>();

            int runStart = 0;
            int runLength = 0;
            int currentIndex = 0;
            int listSize = list.Count;

            int previousRunDirection = 0;
            var previousElement = list[0];

            while (currentIndex < listSize)
            {
                var nextElement = list[currentIndex];
                var runDirection = Math.Sign(Compare(previousElement, nextElement));
                if (previousRunDirection != 0 && previousRunDirection != runDirection)
                {
                    sortRuns.Add(GetSortRun(list, runStart, runLength, previousRunDirection));
                    previousRunDirection = 0;
                    runStart = currentIndex;
                    runLength = 1;
                }
                else
                {
                    previousRunDirection = runDirection;
                    runLength++;
                }

                previousElement = nextElement;
                currentIndex++;
            }

            sortRuns.Add(GetSortRun(list, runStart, runLength, previousRunDirection));
            return sortRuns;
        }

        private static SortRun GetSortRun(IList<T> list, int runStart, int runLength, int runDirection)
        {
            var sortRun = new SortRun(runStart, runLength);
            if (runDirection > 0)
                InvertRun(list, sortRun);
            return sortRun;
        }

        private static void InvertRun(IList<T> list, SortRun sortRun)
        {
            int leftIndex = sortRun.Start;
            int rightIndex = sortRun.Start + sortRun.Length - 1;
            int swapCount = sortRun.Length / 2;
            for (int i = 0; i < swapCount; i++)
                list.Swap(leftIndex++, rightIndex--);
        }

        int GetMinrun(int n)
        {
            int r = 0;           /* станет 1 если среди сдвинутых битов будет хотя бы 1 ненулевой */
            while (n >= 64)
            {
                r |= n & 1;
                n >>= 1;
            }
            return n + r;
        }
    }
}
