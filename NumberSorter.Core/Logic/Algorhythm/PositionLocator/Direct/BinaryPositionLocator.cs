using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class BinaryPositionLocator<T> : GenericPositionLocator<T>
    {
        public BinaryPositionLocator(IComparer<T> comparer) : base(comparer) { }

        public override int FindFirstPosition(IList<T> list, T element, int runStart, int length)
        {
            if (length == 0)
                return runStart;
            return BinarySearchFirst(list, element, runStart, runStart + length - 1);
        }

        protected int BinarySearchFirst(IList<T> list, T elementToInsert, int low, int high)
        {
            if (high <= low)
                return (Compare(list[low], elementToInsert) < 0) ? low + 1 : low;

            int index = (low + high) / 2;
            int comparassion = Compare(list[index], elementToInsert);
            if (comparassion < 0)
                return BinarySearchFirst(list, elementToInsert, index + 1, high);
            else
                return BinarySearchFirst(list, elementToInsert, low, index - 1);
        }

        public override int FindLastPosition(IList<T> list, T element, int runStart, int length)
        {
            if (length == 0)
                return runStart;
            return BinarySearchLast(list, element, runStart, runStart + length - 1);
        }

        protected int BinarySearchLast(IList<T> list, T elementToInsert, int low, int high)
        {
            if (high <= low)
                return (Compare(list[low], elementToInsert) <= 0) ? low + 1 : low;

            int index = (low + high) / 2;
            int comparassion = Compare(list[index], elementToInsert);
            if (comparassion <= 0)
                return BinarySearchLast(list, elementToInsert, index + 1, high);
            else
                return BinarySearchLast(list, elementToInsert, low, index - 1);
        }
    }
}
