using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class MSDRadixSortTests : SortTestsBase
    {
        protected override IIntegerSortAlgorhythm GetIntAlgorhythm()
        {
            return new MSDRadixSort();
        }
    }
}
