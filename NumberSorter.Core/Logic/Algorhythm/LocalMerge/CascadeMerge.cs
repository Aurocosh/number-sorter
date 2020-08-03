using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class CascadeMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly ILocalRotationAlgothythm<T> _localMergeAlgothythm;

        public CascadeMerge(IComparer<T> comparer) : base(comparer)
        {
            _localMergeAlgothythm = new RecursiveInPlaceRotation<T>();
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length + secondRun.Length < 2)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            MergeTemp(list, firstRun, secondRun, secondRun.LastIndex + 1);
        }

        private int MergeTemp(IList<T> list, SortRun firstRun, SortRun secondRun, int firstSepIndex)
        {
            int firstIndex = firstRun.Start;
            int firstLength = firstRun.Length;

            int secondIndex = secondRun.Start;
            int secondLength = secondRun.Length;

            T nextFromSecond = list[secondIndex];
            while (firstIndex < secondIndex && Compare(list[firstIndex], nextFromSecond) <= 0)
                firstIndex++;

            firstLength -= firstIndex - firstRun.FirstIndex;

            int tempIndex = secondIndex;
            int indexLimit = secondIndex + Math.Min(firstLength, secondLength);
            if (secondIndex < indexLimit)
            {
                T nextFromFirst = list[firstIndex];
                do
                    list.Swap(firstIndex++, secondIndex++);
                while (secondIndex < indexLimit && Compare(list[secondIndex], nextFromFirst) < 0);
            }

            int tempLength = secondIndex - tempIndex;
            firstLength -= tempLength;
            secondLength -= tempLength;

            while (firstLength > 0 && secondLength > 0)
            {
                T nextFromFirst = list[firstIndex];
                if (Compare(nextFromFirst, list[secondIndex]) <= 0)
                {
                    FetchNext(list, ref firstIndex, ref firstLength, ref tempIndex, ref tempLength);
                }
                else
                {
                    var left = new SortRun(tempIndex, tempLength);
                    var right = new SortRun(secondIndex, secondLength);

                    int takenFromSecond = MergeTemp(list, left, right, firstIndex);
                    tempLength += takenFromSecond;
                    secondIndex += takenFromSecond;
                    secondLength -= takenFromSecond;

                    FetchNext(list, ref firstIndex, ref firstLength, ref tempIndex, ref tempLength);
                }
            }

            if (tempLength > 0)
            {
                if (firstLength > 0)
                {
                    var first = new SortRun(firstIndex, firstLength);
                    var second = new SortRun(tempIndex, tempLength);

                    _localMergeAlgothythm.Rotate(list, first, second);
                }
                else if (secondLength > 0)
                {
                    var left = new SortRun(tempIndex, tempLength);
                    var right = new SortRun(secondIndex, secondLength);

                    int takenFromSecond = MergeTemp(list, left, right, firstSepIndex);
                    tempLength += takenFromSecond;
                    secondLength -= takenFromSecond;

                    if (firstLength > 0)
                        FetchNext(list, ref firstIndex, ref firstLength, ref tempIndex, ref tempLength);
                }
            }
            else if (tempLength == 0 && firstSepIndex < firstRun.FirstIndex)
            {
                indexLimit = secondIndex + secondLength;
                T nextFromFirst = list[firstSepIndex];
                while (secondIndex < indexLimit && Compare(list[secondIndex], nextFromFirst) < 0)
                {
                    secondIndex++;
                    secondLength--;
                }
            }

            return secondRun.Length - secondLength;
        }

        private void FetchNext(IList<T> list, ref int firstIndex, ref int firstLength, ref int tempIndex, ref int tempLength)
        {
            if (firstLength < tempLength)
            {
                var first = new SortRun(firstIndex, firstLength);
                var second = new SortRun(tempIndex, tempLength);

                _localMergeAlgothythm.Rotate(list, first, second);
                tempIndex = firstIndex + tempLength;
                tempLength = firstLength;
                firstLength = 0;
            }
            else
            {
                int fromIndex = firstIndex;
                int toIndex = tempIndex;
                int firstIndexLimit = fromIndex + tempLength;
                do
                    list.Swap(fromIndex++, toIndex++);
                while (fromIndex != firstIndexLimit);

                firstIndex += tempLength;
                firstLength -= tempLength;
            }
        }
    }
}
