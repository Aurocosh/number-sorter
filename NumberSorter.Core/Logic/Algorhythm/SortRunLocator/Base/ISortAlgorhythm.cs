using System.Collections.Generic;

namespace NumberSorter.Core.Algorhythm
{
    public interface ISortRunLocator<T>
    {
        void Sort(IList<T> list);
        IComparer<T> GetComparer();
        int Compare(T first, T second);
        int Compare(IList<T> list, int first, int second);
    }
}
