using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base
{
    abstract public class GenericPositionLocator<T> : IPositionLocator<T>
    {
        protected GenericPositionLocator(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        public IComparer<T> Comparer { get; }
        public int Compare(T first, T second) => Comparer.Compare(first, second);
        public int Compare(IList<T> list, int first, int second) => Comparer.Compare(list[first], list[second]);

        public abstract int FindFirstPosition(IList<T> list, T element, int runStart, int length);
        public abstract int FindLastPosition(IList<T> list, T element, int runStart, int length);
    }
}
