using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class DualPivotQuickSortTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new DualPivotQuickSort<int>(comparer, new InsertionSortFactory(), 0);
        }
    }
}
