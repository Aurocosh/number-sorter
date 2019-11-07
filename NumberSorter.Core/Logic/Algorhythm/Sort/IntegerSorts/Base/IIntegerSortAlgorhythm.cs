using System.Collections.Generic;

namespace NumberSorter.Core.Algorhythm
{
    public interface IIntegerSortAlgorhythm
    {
        void Sort(IList<int> list);
        void Sort(IList<int> list, int startingIndex, int length);
    }
}
