using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    abstract public class GenericSortAlgorhythm<T> : ISortAlgorhythm<T>
    {
        private readonly IComparer<T> _comparer;

        protected GenericSortAlgorhythm(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public abstract void Sort(IList<T> list);
        public IComparer<T> GetComparer() => _comparer;
        public int Compare(T first, T second) => _comparer.Compare(first, second);
        public int Compare(IList<T> list, int first, int second) => _comparer.Compare(list[first], list[second]);
    }
}
