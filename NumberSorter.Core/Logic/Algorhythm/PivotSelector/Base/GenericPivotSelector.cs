using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PivotSelector
{
    abstract public class GenericPivotSelector<T> : IPivotSelector<T>
    {
        public IComparer<T> Comparer { get; }

        protected GenericPivotSelector(IComparer<T> comparer)
        {
            Comparer = comparer;
        }

        public abstract int SelectPivot(IList<T> list, int firstIndex, int lastIndex);
        public int Compare(T first, T second) => Comparer.Compare(first, second);
        public int Compare(IList<T> list, int first, int second) => Comparer.Compare(list[first], list[second]);
    }
}
