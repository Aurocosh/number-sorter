using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class StrandSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeAlgothythm<T> LocalMergeAlgorhythm { get; }

        public StrandSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            LocalMergeAlgorhythm = localMergeFactory.GetLocalMerge(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int bufferMaxSize = list.Count;
            var buffer = new T[bufferMaxSize];

            int elementsSorted = 0;
            int elementsUnsorted = length;
            int firstUnsortedIndex = startingIndex;
            int lastBufferIndex = bufferMaxSize - 1;
            int lastUnsortedIndex = startingIndex + length - 1;

            int startingBufferIndex = lastBufferIndex - 1;
            int startingUnsortedIndex = lastUnsortedIndex - 1;

            while (elementsUnsorted > 0)
            {
                T nextInBuffer = list[lastUnsortedIndex];
                buffer[lastBufferIndex] = nextInBuffer;

                int bufferIndex = startingBufferIndex;
                int nextUnsortedIndex = startingUnsortedIndex;
                int unsortedTargetIndex = lastUnsortedIndex;
                while (nextUnsortedIndex >= firstUnsortedIndex)
                {
                    T nextUnsorted = list[nextUnsortedIndex--];
                    if (Compare(nextUnsorted, nextInBuffer) <= 0)
                    {
                        buffer[bufferIndex--] = nextUnsorted;
                        nextInBuffer = nextUnsorted;
                    }
                    else
                    {
                        list[unsortedTargetIndex--] = nextUnsorted;
                    }
                }

                int bufferSize = lastBufferIndex - bufferIndex;
                elementsUnsorted -= bufferSize;

                ListUtility.Copy(buffer, bufferIndex + 1, list, firstUnsortedIndex, bufferSize);

                var leftRun = new SortRun(startingIndex, elementsSorted);
                var rightRun = new SortRun(firstUnsortedIndex, bufferSize);

                LocalMergeAlgorhythm.Merge(list, leftRun, rightRun);

                elementsSorted += bufferSize;
                firstUnsortedIndex += bufferSize;
            }
        }
    }
}
