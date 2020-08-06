using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class SwapWindowMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly int _minRun;
        private readonly int _maxDepth;

        private readonly ILocalMergeAlgothythm<T> _limitMerge;
        private readonly IPositionLocator<T> _positionLocator;
        private readonly ILocalRotationAlgothythm<T> _localRotationAlgothythm;

        public SwapWindowMerge(IComparer<T> comparer, int maxDepth) : base(comparer)
        {
            _minRun = 32;
            _maxDepth = maxDepth;

            _limitMerge = new WindowMerge<T>(comparer);
            _positionLocator = new BinaryPositionLocator<T>(comparer);
            _localRotationAlgothythm = new RecursiveInPlaceRotation<T>();
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;
            InternalMerge(list, firstRun, secondRun, 1);
        }

        private void InternalMerge(IList<T> list, SortRun firstRun, SortRun secondRun, int depth)
        {
            if (firstRun.Length + secondRun.Length < _minRun || depth > _maxDepth)
            {
                _limitMerge.Merge(list, firstRun, secondRun);
                return;
            }

            T firstFromFirst = list[firstRun.FirstIndex];
            T lastFromSecond = list[secondRun.LastIndex];
            if (Compare(lastFromSecond, firstFromFirst) < 0)
            {
                _localRotationAlgothythm.Rotate(list, firstRun, secondRun);
                return;
            }

            depth++;

            int firstIndex = firstRun.Start;
            int lastFirstIndex = firstRun.LastIndex;

            int middleIndex = (firstIndex + lastFirstIndex) / 2;

            T middleFromFirst = list[middleIndex];
            T firstFromSecond = list[secondRun.FirstIndex];
            if (Compare(middleFromFirst, firstFromSecond) <= 0)
            {
                int newStart = middleIndex + 1;
                while (newStart < secondRun.FirstIndex && Compare(middleFromFirst, list[newStart]) == 0)
                    newStart++;

                var left = new SortRun(newStart, secondRun.FirstIndex - newStart);
                if (Compare(list[left.LastIndex], firstFromSecond) > 0)
                    InternalMerge(list, left, secondRun, depth);
                return;
            }

            while (middleIndex > firstIndex && Compare(middleFromFirst, list[middleIndex - 1]) == 0)
                middleIndex--;

            int positionInSecond = _positionLocator.FindFirstPosition(list, middleFromFirst, secondRun.Start, secondRun.Length) - 1;

            int leftLengthB = secondRun.FirstIndex - middleIndex;
            int leftLengthA = firstRun.Length - leftLengthB;

            int rightLengthA = positionInSecond - secondRun.FirstIndex + 1;
            int rightLengthB = secondRun.Length - rightLengthA;

            var leftRun = new SortRun(middleIndex, leftLengthB);
            var rightRun = new SortRun(secondRun.FirstIndex, rightLengthA);
            _localRotationAlgothythm.Rotate(list, leftRun, rightRun);

            var leftA = new SortRun(firstIndex, leftLengthA);
            var leftB = new SortRun(middleIndex, rightLengthA);
            if (leftA.Length != 0 && leftB.Length != 0 && Compare(list, leftA.LastIndex, leftB.FirstIndex) > 0)
                InternalMerge(list, leftA, leftB, depth);

            var rightA = new SortRun(middleIndex + rightLengthA, leftLengthB);
            var rightB = new SortRun(positionInSecond + 1, rightLengthB);
            if (rightA.Length != 0 && rightB.Length != 0 && Compare(list, rightA.LastIndex, rightB.FirstIndex) > 0)
                InternalMerge(list, rightA, rightB, depth);
        }



        public static bool IsSorted<T>(IList<T> list, SortRun sortRun, IComparer<T> comparer)
        {
            int limit = sortRun.LastIndex;
            for (int i = sortRun.FirstIndex; i < limit; i++)
            {
                var first = list[i];
                var second = list[i + 1];

                int comparassion = comparer.Compare(first, second);
                if (comparassion > 0)
                    return false;
            }
            return true;
        }
    }
}
