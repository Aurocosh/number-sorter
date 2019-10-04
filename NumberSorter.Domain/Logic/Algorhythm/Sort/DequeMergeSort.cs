using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Logic.Algorhythm
{
    public class DequeMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private readonly LinkedList<T> _leftPointerDeque;

        public DequeMergeSort(IComparer<T> comparer) : base(comparer)
        {
            _leftPointerDeque = new LinkedList<T>();
        }

        private sealed class ArrayHalves<K>
        {
            public SortRun First { get; }
            public SortRun Second { get; }

            public ArrayHalves(SortRun first, SortRun second)
            {
                First = first;
                Second = second;
            }
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

            var halvesOfSortRun = SplitSortRun(sortRun);
            MergeSort(list, halvesOfSortRun.First);
            MergeSort(list, halvesOfSortRun.Second);

            Merge(list, halvesOfSortRun.First, halvesOfSortRun.Second);
        }

        private static ArrayHalves<T> SplitSortRun(SortRun sortRun)
        {
            if (sortRun.Length == 0)
                return new ArrayHalves<T>(sortRun, sortRun);
            if (sortRun.Length == 1)
                return new ArrayHalves<T>(sortRun, new SortRun(sortRun.Start, 0));


            int firstIndex = sortRun.Start;
            int firstLength = sortRun.Length / 2;

            int secondIndex = sortRun.Start + firstLength;
            int secondLength = sortRun.Length - firstLength;

            var firstSortRun = new SortRun(firstIndex, firstLength);
            var secondSortRun = new SortRun(secondIndex, secondLength);
            return new ArrayHalves<T>(firstSortRun, secondSortRun);
        }

        private void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            int leftInFirst = firstRun.Length;
            int lastSecondIndex = secondRun.Start + secondRun.Length - 1;

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            while (leftInFirst + _leftPointerDeque.Count > 0 && secondIndex <= lastSecondIndex)
            {
                if (leftInFirst > 0)
                {
                    _leftPointerDeque.AddLast(list[firstIndex]);
                    leftInFirst--;
                }

                var nextFromFirst = _leftPointerDeque.First.Value;
                var nextFromSecond = list[secondIndex];


                int comparassion = Compare(nextFromFirst, nextFromSecond);
                if (comparassion > 0)
                {
                    list[firstIndex] = nextFromSecond;
                    secondIndex++;
                }
                else
                {
                    list[firstIndex] = nextFromFirst;
                    _leftPointerDeque.RemoveFirst();
                }

                firstIndex++;
            }

            foreach (T value in _leftPointerDeque)
                list[firstIndex++] = value;
            _leftPointerDeque.Clear();
        }
    }
}
