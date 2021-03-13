using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class FragmentSort<T> : GenericSortAlgorhythm<T>
    {
        private int FragmentSize { get; }
        private ISortAlgorhythm<T> FragmentSortAlgorithm { get; }

        public FragmentSort(IComparer<T> comparer, ISortFactory sortFactory, int fragmentSize) : base(comparer)
        {
            FragmentSortAlgorithm = sortFactory.GetSort(comparer);
            FragmentSize = fragmentSize;
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int indexLimit = startingIndex + length;
            int nextUnsortedIndex = startingIndex;

            int elementsLeft = indexLimit - nextUnsortedIndex;
            int fragmentSize = Math.Min(elementsLeft, FragmentSize);

            FragmentSortAlgorithm.Sort(list, startingIndex, fragmentSize);
            nextUnsortedIndex += fragmentSize;

            var buffer = new T[FragmentSize];
            while (nextUnsortedIndex < indexLimit)
            {
                elementsLeft = indexLimit - nextUnsortedIndex;
                fragmentSize = Math.Min(elementsLeft, FragmentSize);

                FragmentSortAlgorithm.Sort(list, nextUnsortedIndex, fragmentSize);
                ListUtility.Copy(list, nextUnsortedIndex, buffer, 0, fragmentSize);

                int sortedIndex = nextUnsortedIndex - 1;
                int targetIndex = sortedIndex + fragmentSize;

                int fragmentIndex = fragmentSize - 1;

                nextUnsortedIndex += fragmentSize;

                T nextFromSorted = list[sortedIndex];
                T nextFromFragment = buffer[fragmentIndex];
                while (true)
                {
                    if (Compare(nextFromFragment, nextFromSorted) >= 0)
                    {
                        list[targetIndex--] = nextFromFragment;
                        fragmentIndex--;
                        if (fragmentIndex >= 0)
                        {
                            nextFromFragment = buffer[fragmentIndex];
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        list[targetIndex--] = nextFromSorted;
                        sortedIndex--;
                        if (sortedIndex >= startingIndex)
                        {
                            nextFromSorted = list[sortedIndex];
                        }
                        else
                        {
                            int restOfFragment = fragmentIndex + 1;
                            int adjustedTargetIndex = targetIndex - fragmentIndex;

                            ListUtility.Copy(buffer, 0, list, adjustedTargetIndex, restOfFragment);
                            break;
                        }
                    }
                }
            }
        }
    }
}