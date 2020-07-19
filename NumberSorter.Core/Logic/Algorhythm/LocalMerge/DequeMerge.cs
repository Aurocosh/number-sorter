using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class DequeMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly LinkedList<T> _leftPointerDeque;

        public DequeMerge(IComparer<T> comparer) : base(comparer)
        {
            _leftPointerDeque = new LinkedList<T>();
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
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
