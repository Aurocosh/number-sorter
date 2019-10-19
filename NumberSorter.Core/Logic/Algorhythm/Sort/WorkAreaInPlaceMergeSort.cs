using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class WorkAreaInPlaceMergeSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        public WorkAreaInPlaceMergeSort(IComparer<T> comparer) : base(comparer)
        {
        }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            if (length - startingIndex > 1)
            {
                int m = startingIndex + (length - startingIndex) / 2;
                int w = startingIndex + length - m;
                SortHalfOfWorkArea(list, startingIndex, m, w); /* the last half contains sorted elements */

                while (w - startingIndex > 2)
                {
                    int n = w;
                    w = startingIndex + (n - startingIndex + 1) / 2;
                    SortHalfOfWorkArea(list, w, n, startingIndex);  /* the first half of the previous working area contains sorted elements */
                    Merge(list, startingIndex, startingIndex + n - w, n, length, w);
                }
                for (int n = w; n > startingIndex; --n) /*switch to insertion sort*/
                    for (m = n; m < length && Compare(list, m, m - 1) < 0; ++m)
                        list.Swap(m, m - 1);
            }
        }

        private void SortHalfOfWorkArea(IList<T> list, int startingIndex, int length, int workIndex)
        {
            if (length - startingIndex > 1)
            {
                int newWorkIndex = startingIndex + (length - startingIndex) / 2;
                Sort(list, startingIndex, newWorkIndex);
                Sort(list, newWorkIndex, length);
                Merge(list, startingIndex, newWorkIndex, newWorkIndex, length, workIndex);
            }
            else
                while (startingIndex < length)
                    list.Swap(startingIndex++, workIndex++);
        }

        private void Merge(IList<T> list, int firstIndex, int firstLimit, int secondIndex, int secondLimit, int workIndex)
        {
            while (firstIndex < firstLimit && secondIndex < secondLimit)
                list.Swap(workIndex++, Compare(list, firstIndex, secondIndex) < 0 ? firstIndex++ : secondIndex++);
            while (firstIndex < firstLimit)
                list.Swap(workIndex++, firstIndex++);
            while (secondIndex < secondLimit)
                list.Swap(workIndex++, secondIndex++);
        }
    }
}
