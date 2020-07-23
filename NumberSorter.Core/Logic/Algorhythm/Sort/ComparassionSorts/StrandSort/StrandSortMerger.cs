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
            int bufferMaxSize = list.Count;
            var buffer = new T[bufferMaxSize];

            var merger = RunMergerFactory.GetMerger(Comparer, list);

            int resultSize = 0;
            int elementsInSource = length;
            while (elementsInSource > 0)
            {
                T nextInBuffer = list[0];
                buffer[0] = nextInBuffer;

                int bufferIndex = 1;
                int sourceNextIndex = 1;
                int sourceTargetIndex = 0;
                int sourceLimitIndex = elementsInSource;

                while (sourceNextIndex < sourceLimitIndex)
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

                int bufferSize = bufferIndex;
                elementsInSource -= bufferSize;

                ListUtility.Copy(buffer, 0, list, startingIndex + elementsInSource, bufferSize);

                var leftRun = new SortRun(startingIndex + elementsInSource, bufferSize);
                merger.Push(leftRun);

                resultSize += bufferSize;
            }

            merger.ForceMerge();
        }
    }
}
