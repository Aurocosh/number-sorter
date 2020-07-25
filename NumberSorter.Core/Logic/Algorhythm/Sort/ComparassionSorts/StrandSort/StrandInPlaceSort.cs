using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class StrandInPlaceSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeFactory LocalMergeFactory { get; }

        public StrandInPlaceSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            LocalMergeFactory = localMergeFactory;
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var localMerge = LocalMergeFactory.GetLocalMerge(Comparer, list);

            int resultSize = 0;
            int firstUnsortedIndex = startingIndex;
            int lastUnsortedIndex = startingIndex + length - 1;
            while (firstUnsortedIndex <= lastUnsortedIndex)
            {
                T nextValue = list[firstUnsortedIndex];
                int nextUnsortedIndex = firstUnsortedIndex + 1;
                int targetIndex = nextUnsortedIndex;
                while (nextUnsortedIndex < lastUnsortedIndex)
                {
                    T nextUnsorted = list[nextUnsortedIndex];
                    if (Compare(nextUnsorted, nextValue) >= 0)
                    {
                        int toIndex = nextUnsortedIndex;
                        if (toIndex != targetIndex)
                        {
                            int fromIndex = nextUnsortedIndex - 1;
                            do
                            {
                                list[toIndex--] = list[fromIndex--];
                            } while (toIndex != targetIndex);
                        }

                        list[targetIndex++] = nextUnsorted;
                        nextValue = nextUnsorted;
                    }
                    nextUnsortedIndex++;
                }

                int newRunSize = targetIndex - firstUnsortedIndex;

                var leftRun = new SortRun(startingIndex, resultSize);
                var rightRun = new SortRun(firstUnsortedIndex, newRunSize);

                localMerge.Merge(list, leftRun, rightRun);

                firstUnsortedIndex += newRunSize;
                resultSize += newRunSize;
            }
        }
    }
}
