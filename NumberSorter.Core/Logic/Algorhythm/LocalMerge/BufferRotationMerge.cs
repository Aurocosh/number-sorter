using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class BufferRotationMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly T[] _buffer;
        private readonly int _bufferMaxSize;

        private readonly IPositionLocator<T> _positionLocator;
        private readonly ILocalRotationAlgothythm<T> _localRotator;

        public BufferRotationMerge(IComparer<T> comparer, IPositionLocatorFactory positionLocatorFactory, ILocalRotationFactory LocalRotatorFactory, IList<T> list) : base(comparer)
        {
            _bufferMaxSize = (int)Math.Max(4, Math.Ceiling(Math.Log(list.Count, 2)));
            _buffer = new T[_bufferMaxSize];
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

                if (moveCount < _bufferMaxSize)
                {
                    ListUtility.Copy(list, secondIndex, _buffer, 0, moveCount);
                    secondIndex += moveCount;
                    unsortedInSecond -= moveCount;

                    int bufferIndex = moveCount;
                    int resultIndex = firstIndex;
                    int spacesInBuffer = _bufferMaxSize - moveCount;

                    while (unsortedInSecond > 0)
                    {
                        nextFromSecond = list[secondIndex];
                        int firstIndexStart = firstIndex;
                        int firstIndexLimit = firstIndex + Math.Min(spacesInBuffer, unsortedInFirst);
                        do
                            _buffer[bufferIndex++] = list[firstIndex++];
                        while (firstIndex < firstIndexLimit && Compare(list[firstIndex], nextFromSecond) <= 0);

                        int movedFromFirst = firstIndex - firstIndexStart;
                        spacesInBuffer -= movedFromFirst;
                        unsortedInFirst -= movedFromFirst;

                        if (spacesInBuffer == 0 || unsortedInFirst == 0)
                            break;

                        nextFromFirst = list[firstIndex];
                        int secondIndexStart = secondIndex;
                        int secondIndexLimit = secondIndex + Math.Min(spacesInBuffer, unsortedInSecond);
                        do
                            _buffer[bufferIndex++] = list[secondIndex++];
                        while (secondIndex < secondIndexLimit && Compare(list[secondIndex], nextFromFirst) <= 0);

                        int movedFromSecond = secondIndex - secondIndexStart;
                        spacesInBuffer -= movedFromSecond;
                        unsortedInSecond -= movedFromSecond;

                        if (spacesInBuffer == 0 || unsortedInSecond == 0)
                            break;
                    }
                    int elementsInBuffer = bufferIndex;
                    ClearBuffer(list, firstIndex, secondIndex, resultIndex, unsortedInFirst, elementsInBuffer);
                    firstIndex = resultIndex + elementsInBuffer;
                }
                else
                {
                    var leftRun = new SortRun(firstIndex, unsortedInFirst);
                    var rightRun = new SortRun(secondIndex, moveCount);

                    _localRotator.Rotate(list, leftRun, rightRun);

                    firstIndex += moveCount;
                    secondIndex += moveCount;
                    unsortedInSecond -= moveCount;
                }

                if (unsortedInSecond == 0)
                    break;
            }

            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }

        private void ClearBuffer(IList<T> list, int firstIndex, int secondIndex, int resultIndex, int firstLength, int elementsInBuffer)
        {
            int targetIndex = secondIndex - 1;
            int sourceIndex = firstIndex + firstLength - 1;
            int targetIndexLimit = targetIndex - firstLength;
            while (targetIndex > targetIndexLimit)
                list[targetIndex--] = list[sourceIndex--];

            ListUtility.Copy(_buffer, 0, list, resultIndex, elementsInBuffer);
        }
    }
}