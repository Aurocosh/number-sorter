using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class RelativeMerge<T> : GenericMergeAlgorhythm<T>
    {
        private T[] _buffer;
        private IPositionLocator<T> PositionLocator { get; }
        private IPositionLocator<T> InversePositionLocator { get; }

        public RelativeMerge(IComparer<T> comparer, IPositionLocatorFactory positionLocatorFactory, IPositionLocatorFactory inversePositionLocatorFactory, IList<T> list) : base(comparer)
        {
            _buffer = Array.Empty<T>();
            PositionLocator = positionLocatorFactory.GetPositionLocator(comparer);
            InversePositionLocator = inversePositionLocatorFactory.GetPositionLocator(comparer);
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;

            int lastFirstIndex = firstRun.LastIndex;
            int lastSecondIndex = secondRun.LastIndex;

            int unsortedInFirst = firstRun.Length;
            int unsortedInSecond = secondRun.Length;

            T nextFromSecond = list[secondIndex];
            int firstPosition = PositionLocator.FindLastPosition(list, nextFromSecond, firstIndex, unsortedInFirst);

            int skipCount = firstPosition - firstIndex;
            firstIndex += skipCount;
            unsortedInFirst -= skipCount;

            T nextFromFirst = list[lastFirstIndex];
            int secondPosition = PositionLocator.FindFirstPosition(list, nextFromFirst, secondIndex, unsortedInSecond);

            int lengthChange = lastSecondIndex - secondPosition + 1;
            unsortedInSecond -= lengthChange;


            if (unsortedInFirst <= unsortedInSecond)
            {
                if (_buffer.Length < unsortedInFirst)
                    _buffer = new T[unsortedInFirst];

                int bufferIndex = 0;
                var buffer = _buffer;
                ListUtility.Copy(list, firstIndex, buffer, 0, unsortedInFirst);

                while (true)
                {
                    nextFromFirst = buffer[bufferIndex];
                    secondPosition = PositionLocator.FindLastPosition(list, nextFromFirst, secondIndex, unsortedInSecond);

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
            }
            else
            {
                if (_buffer.Length < unsortedInSecond)
                    _buffer = new T[unsortedInSecond];

                int bufferIndex = unsortedInSecond - 1;
                var buffer = _buffer;
                ListUtility.Copy(list, secondIndex, buffer, 0, unsortedInSecond);

                int firststSourceIndex = firstIndex + unsortedInFirst - 1;
                secondIndex += unsortedInSecond;

                while (true)
                {
                    nextFromSecond = buffer[bufferIndex];
                    firstPosition = InversePositionLocator.FindFirstPosition(list, nextFromSecond, firstIndex, unsortedInFirst);
                    int copyCount = firststSourceIndex - firstPosition + 1;
                    ListUtility.CopyReversed(list, firststSourceIndex - copyCount + 1, list, secondIndex - copyCount, copyCount);
                    secondIndex -= copyCount;
                    firststSourceIndex -= copyCount;
                    unsortedInFirst -= copyCount;
                    if (unsortedInFirst == 0)
                        break;

                    nextFromFirst = list[firststSourceIndex];
                    secondPosition = InversePositionLocator.FindFirstPosition(buffer, nextFromFirst, 0, unsortedInSecond);

                    copyCount = bufferIndex - secondPosition + 1;
                    ListUtility.Copy(buffer, bufferIndex - copyCount + 1, list, secondIndex - copyCount, copyCount);
                    secondIndex -= copyCount;
                    bufferIndex -= copyCount;

                    unsortedInSecond -= copyCount;
                    if (unsortedInSecond == 0)
                        break;
                }

                ListUtility.Copy(buffer, bufferIndex - unsortedInSecond + 1, list, secondIndex - unsortedInSecond, unsortedInSecond);
            }



            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }

    }
}
