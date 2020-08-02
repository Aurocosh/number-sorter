using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class CompositPositionLocator<T> : GenericPositionLocator<T>
    {
        private int LinearCount { get; }

        public CompositPositionLocator(IComparer<T> comparer, int linearCount) : base(comparer)
        {
            LinearCount = linearCount;
        }

        public override int FindFirstPosition(IList<T> list, T element, int runStart, int length)
        {
            int index = runStart;
            int indexLimit = runStart + Math.Min(LinearCount, length);

            while (index < indexLimit)
            {
                if (Compare(list[index], element) >= 0)
                    return index;
                index++;
            }

            int low = index;
            int high = runStart + length - 1;
            if (low > high)
                return low;

            while (high > low)
            {
                index = (low + high) / 2;
                int comparassion = Compare(list[index], element);
                if (comparassion < 0)
                    low = index + 1;
                else
                    high = index - 1;
            }

            return (Compare(list[low], element) < 0) ? low + 1 : low;
        }

        public override int FindLastPosition(IList<T> list, T element, int runStart, int length)
        {
            int index = runStart;
            int indexLimit = runStart + length;

            while (index != indexLimit)
            {
                if (Compare(list[index], element) > 0)
                    return index;
                index++;
            }


            int low = index;
            int high = runStart + length - 1;
            if (low > high)
                return low;

            if (length == 0)
                return runStart;

            while (high > low)
            {
                index = (low + high) / 2;
                int comparassion = Compare(list[index], element);
                if (comparassion <= 0)
                    low = index + 1;
                else
                    high = index - 1;
            }

            return (Compare(list[low], element) <= 0) ? low + 1 : low;
        }
    }
}
