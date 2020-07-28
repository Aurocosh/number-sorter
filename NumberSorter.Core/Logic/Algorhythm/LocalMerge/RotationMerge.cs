using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class RotationMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly IPositionLocator<T> _positionLocator;
        private readonly ILocalRotationAlgothythm<T> _localRotator;

        public RotationMerge(IComparer<T> comparer, IPositionLocatorFactory positionLocatorFactory, ILocalRotationFactory LocalRotatorFactory, IList<T> list) : base(comparer)
        {
            _localRotator = LocalRotatorFactory.GetLocalRotator(comparer, list);
            _positionLocator = positionLocatorFactory.GetPositionLocator(comparer);
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            int unsortedInFirst = firstRun.Length;
            int unsortedInSecond = secondRun.Length;

            while (true)
            {
                T nextFromSecond = list[secondIndex];
                int firstPosition = _positionLocator.FindLastPosition(list, nextFromSecond, firstIndex, unsortedInFirst);

                int skipCount = firstPosition - firstIndex;
                firstIndex += skipCount;
                unsortedInFirst -= skipCount;

                if (unsortedInFirst == 0)
                    break;

                T nextFromFirst = list[firstIndex];
                int secondPosition = _positionLocator.FindLastPosition(list, nextFromFirst, secondIndex, unsortedInSecond);
                int moveCount = secondPosition - secondIndex;

                var leftRun = new SortRun(firstIndex, unsortedInFirst);
                var rightRun = new SortRun(secondIndex, moveCount);

                _localRotator.Rotate(list, leftRun, rightRun);

                firstIndex += moveCount;
                secondIndex += moveCount;
                unsortedInSecond -= moveCount;

                if (unsortedInSecond == 0)
                    break;
            }

            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }
    }
}