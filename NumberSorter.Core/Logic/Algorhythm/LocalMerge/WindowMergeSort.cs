using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class WindowMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly ILocalRotationAlgothythm<T> _localMergeAlgothythm;

        public WindowMerge(IComparer<T> comparer) : base(comparer)
        {
            _localMergeAlgothythm = new RecursiveInPlaceRotation<T>();
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;
            int lastSecondIndex = secondRun.LastIndex;

            int tempLength = 0;
            int tempStartIndex = secondRun.Start;
            int firstSourceIndex = firstRun.Start;

            var nextFromFirst = list[firstIndex];
            var nextFromSecond = list[secondIndex];

            while (firstSourceIndex < secondIndex && secondIndex <= lastSecondIndex)
            {
                if (Compare(nextFromFirst, nextFromSecond) > 0)
                {
                    list[secondIndex] = list[firstIndex];
                    list[firstIndex] = nextFromSecond;

                    if (firstSourceIndex > tempStartIndex)
                        RotateTemporary(list, tempStartIndex, tempLength, firstSourceIndex);

                    firstSourceIndex = tempStartIndex;

                    secondIndex++;
                    tempLength++;

                    if (secondIndex <= lastSecondIndex)
                        nextFromSecond = list[secondIndex];
                }
                else if (firstIndex != firstSourceIndex)
                {
                    list[firstSourceIndex] = list[firstIndex];
                    list[firstIndex] = nextFromFirst;

                    firstSourceIndex = WrapTempIndex(firstSourceIndex + 1, tempStartIndex, secondIndex - 1);
                    nextFromFirst = list[firstSourceIndex];
                }
                else
                {
                    firstSourceIndex++;
                    nextFromFirst = list[firstSourceIndex];
                }

                firstIndex++;
                if (firstIndex == tempStartIndex && tempLength > 0)
                {
                    if (firstSourceIndex != tempStartIndex)
                    {
                        RotateTemporary(list, tempStartIndex, tempLength, firstSourceIndex);
                        firstSourceIndex = firstIndex;
                    }
                    tempStartIndex = secondIndex;
                    tempLength = 0;
                }
            }

            if (firstIndex != firstSourceIndex)
            {
                var leftRun = new SortRun(firstIndex, tempStartIndex - firstIndex);
                var rightRun = new SortRun(tempStartIndex, tempLength);

                _localMergeAlgothythm.Rotate(list, leftRun, rightRun);
            }
        }

        private void RotateTemporary(IList<T> list, int tempStartIndex, int tempLength, int firstSourceIndex)
        {
            var firstLength = firstSourceIndex - tempStartIndex;
            var secondLength = tempLength - firstLength;

            var leftRun = new SortRun(tempStartIndex, firstLength);
            var rightRun = new SortRun(firstSourceIndex, secondLength);

            _localMergeAlgothythm.Rotate(list, leftRun, rightRun);
        }

        private static int WrapTempIndex(int index, int start, int max)
        {
            return index > max ? start : index;
        }
    }
}
