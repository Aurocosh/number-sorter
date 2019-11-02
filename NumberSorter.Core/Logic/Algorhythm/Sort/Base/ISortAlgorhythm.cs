using System.Collections.Generic;

namespace NumberSorter.Core.Algorhythm
{
    public interface ISortAlgorhythm<T> : IComparingAlgorhythm<T>
    {
        void Sort(IList<T> list);
    }
}
