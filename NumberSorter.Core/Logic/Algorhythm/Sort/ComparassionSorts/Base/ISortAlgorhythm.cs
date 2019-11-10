using System.Collections.Generic;

namespace NumberSorter.Core.Algorhythm
{
    public interface ISortAlgorhythm<T> : IComparingAlgorhythm<T>
    {
        void Sort(IList<T> list);
        void Sort(IList<T> list, int startingIndex, int length);
    }
}
