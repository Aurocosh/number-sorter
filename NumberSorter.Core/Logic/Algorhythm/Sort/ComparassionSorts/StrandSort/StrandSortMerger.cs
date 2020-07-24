using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class StrandSortMerger<T> : GenericSortAlgorhythm<T>
    {
        private IRunMergerFactory RunMergerFactory { get; }

        public StrandSortMerger(IComparer<T> comparer, IRunMergerFactory runMergerFactory) : base(comparer)
        {
            RunMergerFactory = runMergerFactory;
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var merger = RunMergerFactory.GetMerger(Comparer, list);

            int bufferMaxSize = list.Count;
            var buffer = new T[bufferMaxSize];

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

                var rightRun = new SortRun(firstUnsortedIndex, bufferSize);
                merger.Push(rightRun);

                firstUnsortedIndex += bufferSize;
            }

            merger.ForceMerge();
        }
    }
}
