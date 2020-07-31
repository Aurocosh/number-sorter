using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class InvBiasedBinaryPositionLocator<T> : BinaryPositionLocator<T>
    {
        private int IndexBias { get; }
        private int MinLength { get; }

        public InvBiasedBinaryPositionLocator(IComparer<T> comparer, int indexBias) : base(comparer)
        {
            IndexBias = indexBias;
            MinLength = IndexBias * 3;
        }

        #region FirstPosition

        public override int FindFirstPosition(IList<T> list, T element, int runStart, int length)
        {
            switch (length)
            {
                case 0:
                    return runStart;
                case 1:
                    return Compare(list[runStart], element) < 0 ? runStart + 1 : runStart;
            }

            int index = runStart + length - 2;
            int comparassion = Compare(list[index], element);
            if (comparassion < 0)
                return BiasedBinarySearchFirstHi(list, element, index + 1, runStart + length - 1, length);
            else
                return BinarySearchFirstLo(list, element, runStart, index - 1);
        }

        private int BiasedBinarySearchFirstHi(IList<T> list, T elementToInsert, int low, int high, int length)
        {
            if (high <= low)
                return (Compare(list[high], elementToInsert) < 0) ? high + 1 : high;

            int bias = length < MinLength ? 0 : IndexBias;

            int index = high - bias;
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
            switch (length)
            {
                case 0:
                    return runStart;
                case 1:
                    return Compare(list[runStart], element) <= 0 ? runStart + 1 : runStart;
            }

            int index = runStart + length - 2;
            int comparassion = Compare(list[index], element);
            if (comparassion <= 0)
                return BiasedBinarySearchLast(list, element, index + 1, runStart + length - 1, length);
            else
                return BinarySearchLastLo(list, element, runStart, index - 1);
        }

        private int BiasedBinarySearchLast(IList<T> list, T elementToInsert, int low, int high, int length)
        {
            if (high <= low)
                return (Compare(list[high], elementToInsert) <= 0) ? high + 1 : high;

            int bias = length < MinLength ? 0 : IndexBias;

            int index = high - bias;
            int comparassion = Compare(list[index], elementToInsert);
            if (comparassion <= 0)
                return BinarySearchLastHi(list, elementToInsert, index + 1, high);
            else
                return BinarySearchLastLo(list, elementToInsert, low, index - 1);
        }

        #endregion
    }
}
