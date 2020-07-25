using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class StrandFixedBufferSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeFactory LocalMergeFactory { get; }

        public StrandFixedBufferSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            LocalMergeFactory = localMergeFactory;
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var localMerge = LocalMergeFactory.GetLocalMerge(Comparer, list);

            int bufferMaxSize = (int)Math.Max(4, Math.Ceiling(Math.Log(list.Count, 2)));
            var buffer = new T[bufferMaxSize];

            int resultSize = 0;
            int firstUnsortedIndex = startingIndex;
            int lastUnsortedIndex = startingIndex + length - 1;
            while (firstUnsortedIndex <= lastUnsortedIndex)
            {
                T nextValue = list[firstUnsortedIndex];

                int bufferIndex = 0;
                int nextUnsortedIndex = firstUnsortedIndex + 1;
                int unsortedTargetIndex = nextUnsortedIndex;
                int bufferIndexLimit = bufferMaxSize;

                while (nextUnsortedIndex <= lastUnsortedIndex)
                {
                    T nextUnsorted = list[nextUnsortedIndex];
                    if (Compare(nextUnsorted, nextValue) >= 0)
                    {
                        if (unsortedTargetIndex != nextUnsortedIndex)
                            list[unsortedTargetIndex] = nextUnsorted;
                        nextValue = nextUnsorted;
                        unsortedTargetIndex++;
                    }
                    else
                    {
                        buffer[bufferIndex++] = nextUnsorted;
                        if (bufferIndex == bufferIndexLimit)
                            break;
                    }

                    nextUnsortedIndex++;
                }

                int bufferSize = bufferIndex;
                int newRunSize = unsortedTargetIndex - firstUnsortedIndex;

                ListUtility.Copy(buffer, 0, list, unsortedTargetIndex, bufferSize);

                var leftRun = new SortRun(startingIndex, resultSize);
                var rightRun = new SortRun(firstUnsortedIndex, newRunSize);

                localMerge.Merge(list, leftRun, rightRun);

                resultSize += newRunSize;
                firstUnsortedIndex += newRunSize;
            }
        }
    }
}
