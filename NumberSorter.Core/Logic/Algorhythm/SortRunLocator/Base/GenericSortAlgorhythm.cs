using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    abstract public class GenericRunLocator<T> : ISortRunLocator<T>
    {
        protected GenericRunLocator(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        public IComparer<T> Comparer { get; }
        public int Compare(T first, T second) => Comparer.Compare(first, second);
        public int Compare(IList<T> list, int first, int second) => Comparer.Compare(list[first], list[second]);

        public abstract SortRun FindNextSortRun(IList<T> list, int runStart, int elementsLeft);
    }
}
