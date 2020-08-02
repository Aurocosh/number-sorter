using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class IterativeBinaryPositionLocator<T> : GenericPositionLocator<T>
    {
        public IterativeBinaryPositionLocator(IComparer<T> comparer) : base(comparer) { }

        public override int FindFirstPosition(IList<T> list, T element, int runStart, int length)
        {
            if (length == 0)
                return runStart;

            int low = runStart;
            int high = runStart + length - 1;

            while (high > low)
            {
                int index = (low + high) / 2;
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
            if (length == 0)
                return runStart;

            int low = runStart;
            int high = runStart + length - 1;

            while (high > low)
            {
                int index = (low + high) / 2;
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
