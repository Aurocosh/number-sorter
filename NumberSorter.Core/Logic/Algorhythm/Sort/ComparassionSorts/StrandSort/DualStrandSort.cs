using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class DualStrandSort<T> : GenericSortAlgorhythm<T>
    {
        private IRunMergerFactory RunMergerFactory { get; }

        public DualStrandSort(IComparer<T> comparer, IRunMergerFactory runMergerFactory) : base(comparer)
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
                ListUtility.Copy(buffer, bufferIndex + 1, list, firstUnsortedIndex, bufferSize);
                var directRun = new SortRun(firstUnsortedIndex, bufferSize);
                merger.Push(directRun);

                elementsUnsorted -= bufferSize;
                firstUnsortedIndex += bufferSize;

                if (elementsUnsorted > 0)
                {
                    nextInBuffer = list[lastUnsortedIndex];
                    buffer[0] = nextInBuffer;

                    bufferIndex = 1;
                    nextUnsortedIndex = startingUnsortedIndex;
                    unsortedTargetIndex = lastUnsortedIndex;
                    while (nextUnsortedIndex >= firstUnsortedIndex)
                    {
                        T nextUnsorted = list[nextUnsortedIndex--];
                        if (Compare(nextUnsorted, nextInBuffer) >= 0)
                        {
                            buffer[bufferIndex++] = nextUnsorted;
                            nextInBuffer = nextUnsorted;
                        }
                        else
                        {
                            list[unsortedTargetIndex--] = nextUnsorted;
                        }
                    }

                    bufferSize = bufferIndex;
                    ListUtility.Copy(buffer, 0, list, firstUnsortedIndex, bufferSize);
                    var invertedRun = new SortRun(firstUnsortedIndex, bufferSize);
                    merger.Push(invertedRun);

                    elementsUnsorted -= bufferSize;
                    firstUnsortedIndex += bufferSize;
                }
            }

            merger.ForceMerge();
        }
    }
}
