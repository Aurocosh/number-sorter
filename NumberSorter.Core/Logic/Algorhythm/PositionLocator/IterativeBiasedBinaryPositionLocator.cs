using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class IterativeBiasedBinaryPositionLocator<T> : GenericPositionLocator<T>
    {
        private int IndexBias { get; }
        private int MinLength { get; }

        public IterativeBiasedBinaryPositionLocator(IComparer<T> comparer, int indexBias) : base(comparer)
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

            int low = runStart;
            int high = runStart + length - 1;

            int index = runStart + 1;
            int comparassion = Compare(list[index], element);
            if (comparassion < 0)
                low = index + 1;
            else
                high = index - 1;

            if (high <= low)
                return (Compare(list[high], element) < 0) ? high + 1 : high;

            int bias = length < MinLength ? 0 : IndexBias;

            index = low + bias;
            comparassion = Compare(list[index], element);
            if (comparassion < 0)
                low = index + 1;
            else
                high = index - 1;

            while (high > low)
            {
                index = (low + high) / 2;
                comparassion = Compare(list[index], element);
                if (comparassion < 0)
                    low = index + 1;
                else
                    high = index - 1;
            }

            return (Compare(list[low], element) < 0) ? low + 1 : low;
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

            int low = runStart;
            int high = runStart + length - 1;

            int index = runStart + 1;
            int comparassion = Compare(list[index], element);
            if (comparassion <= 0)
                low = index + 1;
            else
                high = index - 1;

            if (high <= low)
                return (Compare(list[high], element) <= 0) ? high + 1 : high;

            int bias = length < MinLength ? 0 : IndexBias;

            index = low + bias;
            comparassion = Compare(list[index], element);
            if (comparassion <= 0)
                low = index + 1;
            else
                high = index - 1;

            while (high > low)
            {
                index = (low + high) / 2;
                comparassion = Compare(list[index], element);
                if (comparassion <= 0)
                    low = index + 1;
                else
                    high = index - 1;
            }

            return (Compare(list[low], element) < 0) ? low + 1 : low;
        }

        #endregion
    }
}
