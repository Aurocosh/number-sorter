using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class BufferMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly T[] _buffer;
        private readonly int _bufferMaxSize;
        private IPositionLocator<T> PositionLocator { get; }

        public BufferMerge(IComparer<T> comparer, IPositionLocatorFactory positionLocatorFactory, IList<T> list) : base(comparer)
        {
            _bufferMaxSize = (int)Math.Max(4, Math.Ceiling(Math.Log(list.Count, 2)));
            _buffer = new T[_bufferMaxSize];
            PositionLocator = positionLocatorFactory.GetPositionLocator(comparer);
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            var buffer = _buffer;

            int bufferIndex = 0;
            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            int spaceInBuffer = _bufferMaxSize;
            int unsortedInFirst = firstRun.Length;
            int unsortedInSecond = secondRun.Length;

            T nextFromSecond = list[secondIndex];
            int firstPosition = PositionLocator.FindLastPosition(list, nextFromSecond, firstIndex, unsortedInFirst);

            int skipCount = firstPosition - firstIndex;
            firstIndex += skipCount;
            int resultIndex = firstIndex;
            unsortedInFirst -= skipCount;

            while (true)
            {
                T nextFromFirst = list[firstIndex];
                int secondPosition = PositionLocator.FindLastPosition(list, nextFromFirst, secondIndex, unsortedInSecond);
                if (secondPosition > secondIndex)
                {
                    int copyCount = secondPosition - secondIndex;
                    while (copyCount > 0)
                    {
                        int batchSize = Math.Min(copyCount, spaceInBuffer);
                        ListUtility.Copy(list, secondIndex, buffer, bufferIndex, batchSize);

                        copyCount -= batchSize;
                        bufferIndex += batchSize;
                        secondIndex += batchSize;
                        spaceInBuffer -= batchSize;
                        unsortedInSecond -= batchSize;

                        if (spaceInBuffer == 0)
                        {
                            int targetIndex = secondIndex - 1;
                            int sourceIndex = firstIndex + unsortedInFirst - 1;
                            int targetIndexLimit = targetIndex - unsortedInFirst;
                            while (targetIndex > targetIndexLimit)
                                list[targetIndex--] = list[sourceIndex--];

                            ListUtility.Copy(buffer, 0, list, resultIndex, _bufferMaxSize);
                            resultIndex += _bufferMaxSize;
                            firstIndex = resultIndex;

                            bufferIndex = 0;
                            spaceInBuffer = _bufferMaxSize;
                        }
                    }

                    if (unsortedInSecond == 0)
                        break;
                }

                nextFromSecond = list[secondIndex];
                firstPosition = PositionLocator.FindLastPosition(list, nextFromSecond, firstIndex, unsortedInFirst);
                if (firstPosition > firstIndex)
                {
                    int copyCount = firstPosition - firstIndex;
                    while (copyCount > 0)
                    {
                        int batchSize = Math.Min(copyCount, spaceInBuffer);
                        ListUtility.Copy(list, firstIndex, buffer, bufferIndex, batchSize);

                        copyCount -= batchSize;
                        bufferIndex += batchSize;
                        firstIndex += batchSize;
                        spaceInBuffer -= batchSize;
                        unsortedInFirst -= batchSize;

                        if (spaceInBuffer == 0)
                        {
                            int targetIndex = secondIndex - 1;
                            int sourceIndex = firstIndex + unsortedInFirst - 1;
                            int targetIndexLimit = targetIndex - unsortedInFirst;
                            while (targetIndex > targetIndexLimit)
                                list[targetIndex--] = list[sourceIndex--];

                            ListUtility.Copy(buffer, 0, list, resultIndex, _bufferMaxSize);
                            resultIndex += _bufferMaxSize;
                            firstIndex = resultIndex;

                            bufferIndex = 0;
                            spaceInBuffer = _bufferMaxSize;
                        }
                    }

                    if (unsortedInFirst == 0)
                        break;
                }
            }

            {
                int targetIndex = secondIndex - 1;
                int sourceIndex = firstIndex + unsortedInFirst - 1;
                int targetIndexLimit = targetIndex - unsortedInFirst;
                while (targetIndex > targetIndexLimit)
                    list[targetIndex--] = list[sourceIndex--];

                int leftInBuffer = bufferIndex;
                ListUtility.Copy(buffer, 0, list, resultIndex, leftInBuffer);
            }

            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }
    }
}