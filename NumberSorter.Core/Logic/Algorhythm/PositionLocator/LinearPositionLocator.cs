using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator
{
    public class LinearPositionLocator<T> : GenericPositionLocator<T>
    {
        public LinearPositionLocator(IComparer<T> comparer) : base(comparer) { }

        public override int FindPosition(IList<T> list, T element, int runStart, int length)
        {
            int index = runStart;
            int indexLimit = runStart + length;

            while (index != indexLimit && Compare(element, list[index]) > 0)
                index++;
            return index - 1;
        }
    }
}
