using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class JHeapSort<T> : GenericSortAlgorhythm<T>
    {
        private ISortFactory FinalSortFactory { get; }

        public JHeapSort(IComparer<T> comparer, ISortFactory finalSortFactory) : base(comparer)
        {
            FinalSortFactory = finalSortFactory;
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            int indexLimit = startingIndex + length;
            for (int i = indexLimit - 1; i >= startingIndex; i--)
                MaxHeapify(list, i, indexLimit);

            for (int i = indexLimit - 1; i >= startingIndex; i--)
                MinHeapify(list, i, indexLimit);

            FinalSortFactory.Sort(list, startingIndex, length, Comparer);
        }

        private void MaxHeapify(IList<T> list, int currentParentIndex, int indexLimit)
        {
            T parent = list[currentParentIndex];
            int parentIndex = currentParentIndex;
            int childIndex = (2 * (currentParentIndex + 1)) - 1;

            bool done = false;
            while (childIndex < indexLimit && !done)
            {
                if (childIndex < indexLimit - 1 && Compare(list, childIndex, childIndex + 1) >= 0)
                    childIndex++;

                if (Compare(parent, list[childIndex]) < 0)
                {
                    done = true;
                }
                else
                {

                    list[parentIndex] = list[childIndex];
                    parentIndex = childIndex;
                    childIndex = (2 * (parentIndex + 1)) - 1;
                }
            }

            if (parentIndex != currentParentIndex)
                list[parentIndex] = parent;
        }

        private void MinHeapify(IList<T> list, int currentParentIndex, int indexLimit)
        {
            T parent = list[indexLimit - 1 - currentParentIndex];
            int parentIndex = currentParentIndex;
            int childIndex = (2 * (currentParentIndex + 1)) - 1;

            bool done = false;
            while (childIndex < indexLimit && !done)
            {
                if (childIndex < indexLimit - 1 && Compare(list, indexLimit - 1 - childIndex, indexLimit - 1 - (childIndex + 1)) <= 0)
                    childIndex++;

                if (Compare(parent, list[indexLimit - 1 - childIndex]) > 0)
                {
                    done = true;
                }
                else
                {
                    list[indexLimit - 1 - parentIndex] = list[indexLimit - 1 - childIndex];
                    parentIndex = childIndex;
                    childIndex = (2 * (parentIndex + 1)) - 1;
                }
            }

            if (parentIndex != currentParentIndex)
                list[indexLimit - 1 - parentIndex] = parent;
        }
    }
}
