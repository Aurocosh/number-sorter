using System.Collections.Generic;

namespace NumberSorter.Domain.Algorhythm
{
    public interface ISortAlgorhythm<T>
    {
        void Sort(IList<T> list);
        IComparer<T> GetComparer();
        int Compare(T first, T second);
    }
}
