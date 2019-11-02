using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort.Base
{
    public interface IPartialSortFactory : ISortFactory
    {
        IPartialSortAlgorhythm<T> GetPatrialSort<T>(IComparer<T> comparer);
        void Sort<T>(IList<T> list, int startingIndex, int length, IComparer<T> comparer);
    }
}
