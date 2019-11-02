using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class SelectionSortFactory : PartialSortFactory
    {
        public override IPartialSortAlgorhythm<T> GetPatrialSort<T>(IComparer<T> comparer)
        {
            return new SelectionSort<T>(comparer);
        }
    }
}
