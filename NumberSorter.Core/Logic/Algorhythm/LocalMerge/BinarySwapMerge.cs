using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class BinarySwapMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly IPositionLocator<T> _positionLocator;
        private readonly ILocalRotationAlgothythm<T> _localMergeAlgothythm;

        public BinarySwapMerge(IComparer<T> comparer) : base(comparer)
        {
            _positionLocator = new BinaryPositionLocator<T>(comparer);
            _localMergeAlgothythm = new RecursiveInPlaceRotation<T>();
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            T firstFromFirst = list[firstRun.FirstIndex];
            T lastFromSecond = list[secondRun.LastIndex];
            if (Compare(lastFromSecond, firstFromFirst) < 0)
            {
                _localMergeAlgothythm.Rotate(list, firstRun, secondRun);
                return;
            }

            int firstIndex = firstRun.Start;
            int lastFirstIndex = firstRun.LastIndex;

            int middleIndex = (firstIndex + lastFirstIndex) / 2;

            T middleFromFirst = list[middleIndex];
            T firstFromSecond = list[secondRun.FirstIndex];
            if (Compare(middleFromFirst, firstFromSecond) <= 0)
            {
                int newStart = middleIndex + 1;
                var left = new SortRun(newStart, secondRun.FirstIndex - newStart);
                Merge(list, left, secondRun);
            }

            while (middleIndex > firstIndex && Compare(middleFromFirst, list[middleIndex - 1]) == 0)
                middleIndex--;

            int positionInSecond = _positionLocator.FindFirstPosition(list, middleFromFirst, secondRun.Start, secondRun.Length) - 1;

            int leftLengthB = secondRun.FirstIndex - middleIndex;
            int leftLengthA = firstRun.Length - leftLengthB;

            int rightLengthA = positionInSecond - secondRun.FirstIndex + 1;
            int rightLengthB = secondRun.Length - rightLengthA;

            if (positionInSecond >= secondRun.FirstIndex)
            {
                var leftRun = new SortRun(middleIndex, leftLengthB);
                var rightRun = new SortRun(secondRun.FirstIndex, rightLengthA);

                _localMergeAlgothythm.Rotate(list, leftRun, rightRun);
            }

            var leftA = new SortRun(firstIndex, leftLengthA);
            var leftB = new SortRun(middleIndex, rightLengthA);

            Merge(list, leftA, leftB);

            var rightA = new SortRun(middleIndex + rightLengthA, leftLengthB);
            var rightB = new SortRun(positionInSecond + 1, rightLengthB);

            Merge(list, rightA, rightB);
        }
    }
}
