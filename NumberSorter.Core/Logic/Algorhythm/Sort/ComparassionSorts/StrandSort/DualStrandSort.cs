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
            int bufferMaxSize = list.Count;
            var buffer = new T[bufferMaxSize];

            var merger = RunMergerFactory.GetMerger(Comparer, list);

            int resultSize = 0;
            int elementsUnsorted = length;
            while (elementsUnsorted > 0)
            {
                T nextInBuffer = list[startingIndex];
                buffer[0] = nextInBuffer;

                int bufferIndex = 1;
                int sourceNextIndex = 1;
                int sourceTargetIndex = startingIndex;
                int sourceIndexLimit = elementsUnsorted;

                while (sourceNextIndex < sourceIndexLimit)
                {
                    T nextInSource = list[sourceNextIndex++];
                    if (Compare(nextInSource, nextInBuffer) >= 0)
                    {
                        buffer[bufferIndex++] = nextInSource;
                        nextInBuffer = nextInSource;
                    }
                    else
                    {
                        list[sourceTargetIndex++] = nextInSource;
                    }
                }

                int bufferLength = bufferIndex;

                elementsUnsorted -= bufferLength;
                ListUtility.Copy(buffer, 0, list, startingIndex + elementsUnsorted, bufferLength);
                var directRun = new SortRun(startingIndex + elementsUnsorted, bufferLength);
                merger.Push(directRun);

                resultSize += bufferLength;

                if (elementsUnsorted > 0)
                {
                    int bufferLast = buffer.Length - 1;

                    nextInBuffer = list[startingIndex];
                    buffer[bufferLast] = nextInBuffer;

                    bufferIndex = bufferLast - 1;
                    sourceNextIndex = 1;
                    sourceTargetIndex = startingIndex;
                    sourceIndexLimit = elementsUnsorted;

                    while (sourceNextIndex < sourceIndexLimit)
                    {
                        T nextInSource = list[sourceNextIndex++];
                        if (Compare(nextInSource, nextInBuffer) <= 0)
                        {
                            buffer[bufferIndex--] = nextInSource;
                            nextInBuffer = nextInSource;
                        }
                        else
                        {
                            list[sourceTargetIndex++] = nextInSource;
                        }
                    }

                    bufferLength = bufferLast - bufferIndex;

                    elementsUnsorted -= bufferLength;
                    ListUtility.Copy(buffer, bufferIndex + 1, list, startingIndex + elementsUnsorted, bufferLength);
                    var invertedRun = new SortRun(startingIndex + elementsUnsorted, bufferLength);
                    merger.Push(invertedRun);

                    resultSize += bufferLength;
                }
            }

            merger.ForceMerge();
        }
    }
}
