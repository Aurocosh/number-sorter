using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class StrandFixedBufferSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeAlgothythm<T> LocalMergeAlgorhythm { get; }

        public StrandFixedBufferSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            LocalMergeAlgorhythm = localMergeFactory.GetLocalMerge(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int bufferMaxSize = (int)Math.Max(4, Math.Ceiling(Math.Log(list.Count, 2)));
            var buffer = new T[bufferMaxSize];

            int resultSize = 0;
            int elementsUnsorted = length;
            int nextUnsortedIndex = startingIndex;
            while (elementsUnsorted > 0)
            {
                T nextValue = list[nextUnsortedIndex];

                int bufferIndex = 0;
                int sourceNextIndex = nextUnsortedIndex + 1;
                int sourceTargetIndex = sourceNextIndex;
                int sourceIndexLimit = startingIndex + length;
                int bufferIndexLimit = bufferMaxSize;

                while (sourceNextIndex < sourceIndexLimit)
                {
                    T nextUnsorted = list[sourceNextIndex];
                    if (Compare(nextUnsorted, nextValue) >= 0)
                    {
                        if (sourceTargetIndex != sourceNextIndex)
                            list[sourceTargetIndex] = nextUnsorted;
                        nextValue = nextUnsorted;
                        sourceTargetIndex++;
                    }
                    else
                    {
                        buffer[bufferIndex++] = nextUnsorted;
                        if (bufferIndex == bufferIndexLimit)
                            break;
                    }

                    sourceNextIndex++;
                }

                int bufferSize = bufferIndex;
                int newRunSize = sourceTargetIndex - nextUnsortedIndex;

                ListUtility.Copy(buffer, 0, list, sourceTargetIndex, bufferSize);

                var leftRun = new SortRun(startingIndex, resultSize);
                var rightRun = new SortRun(nextUnsortedIndex, newRunSize);

                LocalMergeAlgorhythm.Merge(list, leftRun, rightRun);

                resultSize += newRunSize;
                elementsUnsorted -= newRunSize;
                nextUnsortedIndex += newRunSize;
            }
        }
    }
}
