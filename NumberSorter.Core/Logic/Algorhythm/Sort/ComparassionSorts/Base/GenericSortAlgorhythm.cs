using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    abstract public class GenericSortAlgorhythm<T> : ISortAlgorhythm<T>
    {
        public IComparer<T> Comparer { get; }

        protected GenericSortAlgorhythm(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        public abstract void Sort(IList<T> list);
        public int Compare(T first, T second) => Comparer.Compare(first, second);
        public int Compare(IList<T> list, int first, int second) => Comparer.Compare(list[first], list[second]);
    }
}
