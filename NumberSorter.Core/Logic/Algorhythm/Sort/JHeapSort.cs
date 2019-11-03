using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System;
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

        void MaxHeapify(IList<T> list, int length, int currentParentIndex)
        {
            T parent = list[currentParentIndex];
            int parentIndex = currentParentIndex;
            int childIndex = (2 * (currentParentIndex + 1)) - 1;

            bool done = false;
            while (childIndex < length && !done)
            {
                if (childIndex < length - 1 && Compare(list, childIndex, childIndex + 1) >= 0)
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

        void MinHeapify(IList<T> list, int length, int currentParentIndex)
        {
            T parent = list[length - 1 - currentParentIndex];
            int parentIndex = currentParentIndex;
            int childIndex = (2 * (currentParentIndex + 1)) - 1;

            bool done = false;
            while (childIndex < length && !done)
            {
                if (childIndex < length - 1 && Compare(list, length - 1 - childIndex, length - 1 - (childIndex + 1)) <= 0)
                    childIndex++;

                if (Compare(parent, list[length - 1 - childIndex]) > 0)
                {
                    done = true;
                }
                else
                {
                    list[length - 1 - parentIndex] = list[length - 1 - childIndex];
                    parentIndex = childIndex;
                    childIndex = (2 * (parentIndex + 1)) - 1;
                }
            }

            if (parentIndex != currentParentIndex)
                list[length - 1 - parentIndex] = parent;
        }

        public override void Sort(IList<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
                MaxHeapify(list, list.Count, i);

            for (int i = list.Count - 1; i >= 0; i--)
                MinHeapify(list, list.Count, i);

            FinalSortFactory.Sort(list, Comparer);
        }
    }
}
