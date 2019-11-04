using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class BiasedBinaryPositionLocator<T> : GenericPositionLocator<T>
    {
        private int IndexBias { get; }
        private int MinLength { get; }

        public BiasedBinaryPositionLocator(IComparer<T> comparer, int indexBias) : base(comparer)
        {
            IndexBias = indexBias;
            MinLength = IndexBias * 3;
        }

        #region FirstPosition

        public override int FindFirstPosition(IList<T> list, T element, int runStart, int length)
        {
            int bias = length < MinLength ? 1 : IndexBias;
            return BiasedBinarySearchFirst(list, element, runStart, runStart + length - 1, bias);
        }

        private int BiasedBinarySearchFirst(IList<T> list, T elementToInsert, int low, int high, int bias)
        {
            if (high <= low)
                return (Compare(list[low], elementToInsert) > 0) ? low - 1 : low;

            int index = low + bias;
            int comparassion = Compare(elementToInsert, list[index]);
            if (comparassion == 0)
                return index;
            else if (comparassion > 0)
                return BinarySearchFirst(list, elementToInsert, index + 1, high, high);
            else
                return BinarySearchFirst(list, elementToInsert, low, index - 1, low);
        }

        private int BinarySearchFirst(IList<T> list, T elementToInsert, int low, int high, int validIndex)
        {
            if (high <= low)
                return (Compare(list[validIndex], elementToInsert) > 0) ? validIndex - 1 : validIndex;

            int index = (low + high) / 2;
            int comparassion = Compare(elementToInsert, list[index]);
            if (comparassion == 0)
                return index;
            else if (comparassion > 0)
                return BinarySearchFirst(list, elementToInsert, index + 1, high, high);
            else
                return BinarySearchFirst(list, elementToInsert, low, index - 1, low);
        }

        #endregion

        #region LastPostion

        public override int FindLastPosition(IList<T> list, T element, int runStart, int length)
        {
            int bias = length < MinLength ? 1 : IndexBias;
            return BiasedBinarySearchLast(list, element, runStart, runStart + length - 1, bias);
        }

        private int BiasedBinarySearchLast(IList<T> list, T elementToInsert, int low, int high, int bias)
        {
            if (high <= low)
                return (Compare(list[high], elementToInsert) > 0) ? high - 1 : high;

            int index = low + bias;
            int comparassion = Compare(elementToInsert, list[index]);
            if (comparassion >= 0)
                return BinarySearchLast(list, elementToInsert, index + 1, high, high);
            else
                return BinarySearchLast(list, elementToInsert, low, index - 1, low);
        }

        private int BinarySearchLast(IList<T> list, T elementToInsert, int low, int high, int validIndex)
        {
            if (high <= low)
                return (Compare(list[validIndex], elementToInsert) > 0) ? validIndex - 1 : validIndex;

            int index = (low + high) / 2;
            int comparassion = Compare(elementToInsert, list[index]);
            if (comparassion >= 0)
                return BinarySearchLast(list, elementToInsert, index + 1, high, high);
            else
                return BinarySearchLast(list, elementToInsert, low, index - 1, low);
        }

        #endregion
    }
}
