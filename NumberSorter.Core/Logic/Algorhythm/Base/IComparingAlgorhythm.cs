using System.Collections.Generic;

namespace NumberSorter.Core.Algorhythm
{
    public interface IComparingAlgorhythm<T>
    {
        IComparer<T> Comparer { get; }
        int Compare(T first, T second);
        int Compare(IList<T> list, int first, int second);
    }
}
