using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort.Base
{
    public abstract class PartialSortFactory : IPartialSortFactory
    {
        public abstract IPartialSortAlgorhythm<T> GetPatrialSort<T>(IComparer<T> comparer);

        public ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer) => GetPatrialSort(comparer);

        public void Sort<T>(IList<T> list, IComparer<T> comparer)
        {
            var algorhythm = GetPatrialSort(comparer);
            algorhythm.Sort(list);
        }

        public void Sort<T>(IList<T> list, int startingIndex, int length, IComparer<T> comparer)
        {
            var algorhythm = GetPatrialSort(comparer);
            algorhythm.Sort(list, startingIndex, length);
        }
    }
}
