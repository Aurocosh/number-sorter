using NumberSorter.Domain.Logic.Container;
using System;
using System.Collections.Generic;

namespace NumberSorter.Domain.Logic.Algorhythm
{
    public class TimSort<T> : GenericSortAlgorhythm<T>
    {
        private InsertionSort<T> _insertionSort;

        public TimSort(IComparer<T> comparer) : base(comparer)
        {
            _insertionSort = new InsertionSort<T>(comparer);
        }

        private enum ArrayLeftover
        {
            First,
            Second,
            Both
        }

        private sealed class SortRun
        {
            public int Start { get; }
            public int Length { get; }

            public SortRun() : this(0, 0) { }

            public SortRun(int start, int length)
            {
                Start = start;
                Length = length;
            }
        }

        private sealed class SortRunStack
        {
            private readonly LinkedList<SortRun> _sortRuns;

            private SortRun _sortRunA;
            private SortRun _sortRunB;
            private SortRun _sortRunC;

            public SortRunStack()
            {
                _sortRuns = new LinkedList<SortRun>();

                _sortRunA = new SortRun();
                _sortRunB = new SortRun();
                _sortRunC = new SortRun();
            }

            public int Count => _sortRuns.Count;

            public void Push(SortRun sortRun)
            {
                _sortRuns.AddLast(_sortRunC);
                _sortRunC = _sortRunB;
                _sortRunB = _sortRunA;
                _sortRunA = sortRun;
            }

            public SortRun Pop()
            {
                if (_sortRuns.Count == 0)
                    return null;
                var sortRun = _sortRunA;
                _sortRunA = _sortRunB;
                _sortRunB = _sortRunC;
                _sortRunC = _sortRuns.Last.Value;
                return sortRun;
            }

            public bool MergeNeeded()
            {
                return (_sortRunA.Length > _sortRunB.Length + _sortRunC.Length) && (_sortRunB.Length > _sortRunC.Length);
            }
        }

        public override void Sort(IList<T> list)
        {
            if (list.Count < 64)
            {
                _insertionSort.Sort(list);
                return;
            }

            var sortRuns = new LinkedList<SortRun>();

            int currentIndex = 0;
            int listSize = list.Count;
            int minimalRunLength = GetMinrun(listSize);
            while (currentIndex < listSize)
            {
                var sortRun = FindNextSortRun(list, currentIndex);
                if (sortRun.Length < minimalRunLength)
                {
                    sortRun = new SortRun(sortRun.Start, minimalRunLength);
                    _insertionSort.Sort(list, sortRun.Start, sortRun.Length);
                }
                sortRuns.AddLast(sortRun);
                currentIndex += sortRun.Length;
            }

            var runStack = new SortRunStack();
            while (sortRuns.Count > 0 && runStack.Count > 1)
            {
                if (runStack.MergeNeeded())
                {
                    var rightSortRun = runStack.Pop();
                    var leftSortRun = runStack.Pop();
                    var mergedRun = MergeRuns(list, leftSortRun, rightSortRun);
                    runStack.Push(mergedRun);
                }
                else
                {
                    runStack.Push(sortRuns.Last.Value);
                }
            }
        }

        private SortRun MergeRuns(IList<T> list, SortRun leftRun, SortRun rightRun)
        {
            return new SortRun();
        }

        private SortRun FindNextSortRun(IList<T> list, int runStart)
        {
            int runLength = 0;
            int listSize = list.Count;
            int currentIndex = runStart;

            int previousRunDirection = 0;
            var previousElement = list[runStart];

            while (currentIndex < listSize)
            {
                var nextElement = list[currentIndex];
                var runDirection = Math.Sign(Compare(previousElement, nextElement));
                if (previousRunDirection == 0 || previousRunDirection == runDirection)
                {
                    previousRunDirection = runDirection;
                    previousElement = nextElement;
                    currentIndex++;
                    runLength++;
                }
                else
                {
                    break;
                }
            }

            return GetSortRun(list, runStart, runLength, previousRunDirection);
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

        private static int GetMinrun(int n)
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
