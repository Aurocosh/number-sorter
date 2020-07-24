using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class StrandInPlaceUnstableSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeAlgothythm<T> LocalMergeAlgorhythm { get; }

        public StrandInPlaceUnstableSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            LocalMergeAlgorhythm = localMergeFactory.GetLocalMerge(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int resultSize = 0;
            int elementsUnsorted = length;
            int nextUnsortedIndex = startingIndex;
            while (elementsUnsorted > 0)
            {
                T nextValue = list[nextUnsortedIndex];
                int sourceNextIndex = nextUnsortedIndex + 1;
                int targetIndex = sourceNextIndex;
                int sourceLimitIndex = nextUnsortedIndex + elementsUnsorted;

                while (sourceNextIndex < sourceLimitIndex)
                {
                    T nextUnsorted = list[sourceNextIndex];
                    if (Compare(nextUnsorted, nextValue) >= 0)
                    {
                        list[sourceNextIndex] = list[targetIndex];
                        list[targetIndex++] = nextUnsorted;
                        nextValue = nextUnsorted;
                    }
                    sourceNextIndex++;
                }

                int newRunSize = targetIndex - nextUnsortedIndex;

                var leftRun = new SortRun(startingIndex, resultSize);
                var rightRun = new SortRun(nextUnsortedIndex, newRunSize);

                LocalMergeAlgorhythm.Merge(list, leftRun, rightRun);

                nextUnsortedIndex += newRunSize;
                elementsUnsorted -= newRunSize;
                resultSize += newRunSize;
            }
        }
    }
}
