using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class TimSortGallopSortLinkedTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var merge = new GallopMergeFactory();
            var merger = new LinkedListMergerFactory(merge);
            var minrunSort = new BinarySortFactory();
            return new TimSort<int>(comparer, merger, minrunSort);
        }
    }
}
