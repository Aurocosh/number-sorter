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
            int lastSecondIndex = secondRun.LastIndex;

            T nextFromSecond = list[secondIndex];
            while (firstIndex < secondIndex && Compare(list[firstIndex], nextFromSecond) <= 0)
                firstIndex++;

            firstLength -= firstIndex - firstRun.FirstIndex;

            int tempIndex = secondIndex;
            int indexLimit = secondIndex + Math.Min(firstLength, secondRun.Length);
            if (secondIndex < indexLimit)
            {
                T nextFromFirst = list[firstIndex];
                do
                    list.Swap(firstIndex++, secondIndex++);
                while (secondIndex < indexLimit && Compare(list[secondIndex], nextFromFirst) < 0);
            }

            int tempLength = secondIndex - tempIndex;
            firstLength -= tempLength;

            while (firstLength > 0 && secondIndex <= lastSecondIndex)
            {
                T nextFromFirst = list[firstIndex];
                if (Compare(nextFromFirst, list[secondIndex]) <= 0)
                {
                    FetchNext(list, ref firstIndex, ref firstLength, ref tempIndex, ref tempLength);
                }
                else
                {
                    var left = new SortRun(tempIndex, tempLength);
                    var right = new SortRun(secondIndex, lastSecondIndex - secondIndex + 1);

                    int takenFromSecond = MergeTemp(list, left, right, firstIndex);
                    tempLength += takenFromSecond;
                    secondIndex += takenFromSecond;

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
                else if (secondIndex <= lastSecondIndex)
                {

                    var left = new SortRun(tempIndex, tempLength);
                    var right = new SortRun(secondIndex, lastSecondIndex - secondIndex + 1);

                    int takenFromSecond = MergeTemp(list, left, right, firstSepIndex);
                    tempLength += takenFromSecond;
                    secondIndex += takenFromSecond;

                    if (firstLength > 0)
                        FetchNext(list, ref firstIndex, ref firstLength, ref tempIndex, ref tempLength);
                }
            }
            else if (tempLength == 0 && firstSepIndex < firstRun.FirstIndex)
            {
                T nextFromFirst = list[firstSepIndex];
                while (secondIndex <= lastSecondIndex && Compare(list[secondIndex], nextFromFirst) < 0)
                    secondIndex++;
            }

            return secondIndex - secondRun.FirstIndex;
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
