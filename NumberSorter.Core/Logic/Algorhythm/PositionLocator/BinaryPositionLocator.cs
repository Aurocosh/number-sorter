using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System;
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
            return BinarySearchFirstLo(list, element, runStart, runStart + length - 1);
        }

        protected int BinarySearchFirstLo(IList<T> list, T elementToInsert, int low, int high)
        {
            if (high <= low)
                return (Compare(list[low], elementToInsert) < 0) ? low + 1 : low;

            int index = (low + high) / 2;
            int comparassion = Compare(list[index], elementToInsert);
            if (comparassion < 0)
                return BinarySearchFirstHi(list, elementToInsert, index + 1, high);
            else
                return BinarySearchFirstLo(list, elementToInsert, low, index - 1);
        }

        protected int BinarySearchFirstHi(IList<T> list, T elementToInsert, int low, int high)
        {
            if (high <= low)
                return (Compare(list[high], elementToInsert) < 0) ? high + 1 : high;

            int index = (low + high) / 2;
            int comparassion = Compare(list[index], elementToInsert);
            if (comparassion < 0)
                return BinarySearchFirstHi(list, elementToInsert, index + 1, high);
            else
                return BinarySearchFirstLo(list, elementToInsert, low, index - 1);
        }

        public override int FindLastPosition(IList<T> list, T element, int runStart, int length)
        {
            if (length == 0)
                return runStart;
            return BinarySearchLastLo(list, element, runStart, runStart + length - 1);
        }

        protected int BinarySearchLastLo(IList<T> list, T elementToInsert, int low, int high)
        {
            if (high <= low)
                return (Compare(list[low], elementToInsert) <= 0) ? low + 1 : low;

            int index = (low + high) / 2;
            int comparassion = Compare(list[index], elementToInsert);
            if (comparassion <= 0)
                return BinarySearchLastHi(list, elementToInsert, index + 1, high);
            else
                return BinarySearchLastLo(list, elementToInsert, low, index - 1);
        }

        protected int BinarySearchLastHi(IList<T> list, T elementToInsert, int low, int high)
        {
            if (high <= low)
                return (Compare(list[high], elementToInsert) <= 0) ? high + 1 : high;

            int index = (low + high) / 2;
            int comparassion = Compare(elementToInsert, list[index]);
            if (comparassion >= 0)
                return BinarySearchLastHi(list, elementToInsert, index + 1, high);
            else
                return BinarySearchLastLo(list, elementToInsert, low, index - 1);
        }
    }
}
