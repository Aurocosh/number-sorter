using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class SliceMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly ILocalRotationAlgothythm<T> _localMergeAlgothythm;

        public SliceMerge(IComparer<T> comparer) : base(comparer)
        {
            _localMergeAlgothythm = new RecursiveInPlaceRotation<T>();
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            MergeTemp(list, firstRun, secondRun, secondRun.LastIndex + 1);
        }

        private int MergeTemp(IList<T> list, SortRun firstRun, SortRun secondRun, int firstSepIndex)
        {
            int firstIndex = firstRun.Start;
            int firstIndexLimit = firstRun.ImdexLimit;

            int secondIndex = secondRun.Start;
            int lastIndexLimit = secondRun.ImdexLimit;

            T nextFromSecond = list[secondIndex];
            while (firstIndex < secondIndex && Compare(list[firstIndex], nextFromSecond) <= 0)
                firstIndex++;

            int tempIndex = secondIndex;
            int indexLimit = secondIndex + Math.Min(firstIndexLimit - firstIndex, secondRun.Length);
            if (secondIndex < indexLimit)
            {
                T nextFromFirst = list[firstIndex];
                do
                    list.Swap(firstIndex++, secondIndex++);
                while (secondIndex < indexLimit && Compare(list[secondIndex], nextFromFirst) < 0);
            }

            while (firstIndex < firstIndexLimit && secondIndex < lastIndexLimit)
            {
                T nextFromFirst = list[firstIndex];
                if (Compare(nextFromFirst, list[secondIndex]) <= 0)
                {
                    FetchNext(list, ref firstIndex, firstIndexLimit, ref tempIndex, secondIndex);
                }
                else
                {
                    var left = new SortRun(tempIndex, secondIndex - tempIndex);
                    var right = new SortRun(secondIndex, lastIndexLimit - secondIndex);

                    int takenFromSecond = MergeTemp(list, left, right, firstIndex);
                    secondIndex += takenFromSecond;

                    FetchNext(list, ref firstIndex, firstIndexLimit, ref tempIndex, secondIndex);
                }
            }

            if (tempIndex < secondIndex)
            {
                if (firstIndex < firstIndexLimit)
                {
                    var first = new SortRun(firstIndex, firstIndexLimit - firstIndex);
                    var second = new SortRun(tempIndex, secondIndex - tempIndex);

                    _localMergeAlgothythm.Rotate(list, first, second);
                }
                else if (secondIndex < lastIndexLimit)
                {
                    var left = new SortRun(tempIndex, secondIndex - tempIndex);
                    var right = new SortRun(secondIndex, lastIndexLimit - secondIndex);

                    int takenFromSecond = MergeTemp(list, left, right, firstSepIndex);
                    secondIndex += takenFromSecond;

                    if (firstIndex < firstIndexLimit)
                        FetchNext(list, ref firstIndex, firstIndexLimit, ref tempIndex, secondIndex);
                }
            }
            else if (tempIndex == secondIndex - 1 && firstSepIndex < firstRun.FirstIndex)
            {
                T nextFromFirst = list[firstSepIndex];
                while (secondIndex < lastIndexLimit && Compare(list[secondIndex], nextFromFirst) < 0)
                    secondIndex++;
            }

            return secondIndex - secondRun.FirstIndex;
        }

        private void FetchNext(IList<T> list, ref int firstIndex, int firstIndexLimit, ref int tempIndex, int secondIndex)
        {
            int firstLength = firstIndexLimit - firstIndex;
            int tempLength = secondIndex - tempIndex;
            if (firstLength < tempLength)
            {
                var first = new SortRun(firstIndex, firstLength);
                var second = new SortRun(tempIndex, tempLength);

                _localMergeAlgothythm.Rotate(list, first, second);
                tempIndex = firstIndex + tempLength;
                firstIndex += firstLength;
            }
            else
            {
                int fromIndex = firstIndex;
                int toIndex = tempIndex;
                int fromIndexLimit = fromIndex + tempLength;
                do
                    list.Swap(fromIndex++, toIndex++);
                while (fromIndex != fromIndexLimit);

                firstIndex += tempLength;
            }
        }
    }
}
