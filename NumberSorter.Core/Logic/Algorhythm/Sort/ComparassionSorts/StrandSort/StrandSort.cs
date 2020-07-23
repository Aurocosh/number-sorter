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
                var rightRun = new SortRun(startingIndex + elementsInSource + bufferSize, resultSize);

                LocalMergeAlgorhythm.Merge(list, leftRun, rightRun);

                resultSize += bufferSize;
            }
        }
    }
}
