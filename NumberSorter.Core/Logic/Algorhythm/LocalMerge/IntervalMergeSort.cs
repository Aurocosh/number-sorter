using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class IntervalMerge<T> : GenericMergeAlgorhythm<T>
    {
        private T[] _buffer;
        private IPositionLocator<T> PositionLocator { get; }

        public IntervalMerge(IComparer<T> comparer, IPositionLocatorFactory positionLocatorFactory, IList<T> list) : base(comparer)
        {
            _buffer = Array.Empty<T>();
            PositionLocator = positionLocatorFactory.GetPositionLocator(comparer);
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

            T nextFromSecond = list[secondIndex];
            int firstPosition = PositionLocator.FindLastPosition(list, nextFromSecond, firstIndex, unsortedInFirst);

            int skipCount = firstPosition - firstIndex;
            firstIndex += skipCount;
            unsortedInFirst -= skipCount;

            int bufferIndex = 0;
            ResiseBufferIfNeeded(unsortedInFirst);

            var buffer = _buffer;
            ListUtility.Copy(list, firstIndex, buffer, 0, unsortedInFirst);

            while (true)
            {
                T nextFromFirst = buffer[bufferIndex];
                int secondPosition = PositionLocator.FindLastPosition(list, nextFromFirst, secondIndex, unsortedInSecond);

                int copyCount = secondPosition - secondIndex;
                ListUtility.Copy(list, secondIndex, list, firstIndex, copyCount);
                firstIndex += copyCount;
                secondIndex += copyCount;

                unsortedInSecond -= copyCount;
                if (unsortedInSecond == 0)
                    break;

                nextFromSecond = list[secondIndex];
                firstPosition = PositionLocator.FindLastPosition(buffer, nextFromSecond, bufferIndex, unsortedInFirst);
                copyCount = firstPosition - bufferIndex;
                ListUtility.Copy(buffer, bufferIndex, list, firstIndex, copyCount);
                firstIndex += copyCount;
                bufferIndex += copyCount;
                unsortedInFirst -= copyCount;
                if (unsortedInFirst == 0)
                    break;
            }

            ListUtility.Copy(buffer, bufferIndex, list, firstIndex, unsortedInFirst);

            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }

        private void ResiseBufferIfNeeded(int minCapacity)
        {
            if (_buffer.Length < minCapacity)
            {
                var newSize = minCapacity;
                newSize |= newSize >> 1;
                newSize |= newSize >> 2;
                newSize |= newSize >> 4;
                newSize |= newSize >> 8;
                newSize |= newSize >> 16;
                newSize++;

                newSize = newSize > minCapacity ? newSize : minCapacity;
                _buffer = new T[newSize];
            }
        }
    }
}
