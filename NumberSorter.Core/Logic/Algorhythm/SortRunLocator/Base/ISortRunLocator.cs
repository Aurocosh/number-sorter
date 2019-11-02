using NumberSorter.Core.Logic.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Algorhythm
{
    public interface ISortRunLocator<T> : IComparingAlgorhythm<T>
    {
        SortRun FindNextSortRun(IList<T> list, int runStart, int length);
    }
}
