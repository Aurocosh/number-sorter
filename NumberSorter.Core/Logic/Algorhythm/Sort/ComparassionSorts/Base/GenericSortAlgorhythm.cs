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

        public virtual void Sort(IList<T> list) => Sort(list, 0, list.Count);
        public abstract void Sort(IList<T> list, int startingIndex, int length);
        public int Compare(T first, T second) => Comparer.Compare(first, second);
        public int Compare(IList<T> list, int first, int second) => Comparer.Compare(list[first], list[second]);
    }
}
