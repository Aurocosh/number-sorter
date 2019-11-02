using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class BinaryPositionLocator<T> : GenericPositionLocator<T>
    {
        public BinaryPositionLocator(IComparer<T> comparer) : base(comparer) { }

        public override int FindPosition(IList<T> list, T element, int runStart, int length)
        {
            return BinarySearch(list, element, runStart, runStart + length - 1);
        }

        private int BinarySearch(IList<T> list, T elementToInsert, int low, int high)
        {
            if (high <= low)
                return (Compare(list[low], elementToInsert) > 0) ? low - 1 : low;

            int index = (low + high) / 2;
            int comparassion = Compare(elementToInsert, list[index]);
            if (comparassion == 0)
                return index;
            else if (comparassion > 0)
                return BinarySearch(list, elementToInsert, index + 1, high);
            else
                return BinarySearch(list, elementToInsert, low, index - 1);
        }
    }
}
