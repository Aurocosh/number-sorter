using NumberSorter.Core.Logic.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Algorhythm
{
    public interface ISortRunLocator<T>
    {
        SortRun FindNextSortRun(IList<T> list, int runStart, int length);
        IComparer<T> Comparer { get; }
        int Compare(T first, T second);
        int Compare(IList<T> list, int first, int second);
    }
}
