using NumberSorter.Core.Logic.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Algorhythm
{
    public interface IPartialSortAlgorhythm<T> : ISortAlgorhythm<T>
    {
        void Sort(IList<T> list, int startingIndex, int length);
    }
}
