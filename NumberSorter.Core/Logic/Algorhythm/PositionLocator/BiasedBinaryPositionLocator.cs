using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class BiasedBinaryPositionLocator<T> : BinaryPositionLocator<T>
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
            if (length < 2)
            {
                if (length == 1 && Compare(list[runStart], element) < 0)
                    return runStart + 1;
                return runStart;
            }
            int bias = length < MinLength ? 1 : IndexBias;
            return BiasedBinarySearchFirst(list, element, runStart, runStart + length - 1, bias);
        }

        private int BiasedBinarySearchFirst(IList<T> list, T elementToInsert, int low, int high, int bias)
        {
            int index = low + bias;
            int comparassion = Compare(list[index], elementToInsert);
            if (comparassion < 0)
                return BinarySearchFirstHi(list, elementToInsert, index + 1, high);
            else
                return BinarySearchFirstLo(list, elementToInsert, low, index - 1);
        }

        #endregion

        #region LastPostion

        public override int FindLastPosition(IList<T> list, T element, int runStart, int length)
        {
            if (length < 2)
            {
                if (length == 1 && Compare(list[runStart], element) <= 0)
                    return runStart + 1;
                return runStart;
            }

            int bias = length < MinLength ? 1 : IndexBias;
            return BiasedBinarySearchLast(list, element, runStart, runStart + length - 1, bias);
        }

        private int BiasedBinarySearchLast(IList<T> list, T elementToInsert, int low, int high, int bias)
        {
            int index = low + bias;
            int comparassion = Compare(list[index], elementToInsert);
            if (comparassion <= 0)
                return BinarySearchLastHi(list, elementToInsert, index + 1, high);
            else
                return BinarySearchLastLo(list, elementToInsert, low, index - 1);
        }

        #endregion
    }
}
