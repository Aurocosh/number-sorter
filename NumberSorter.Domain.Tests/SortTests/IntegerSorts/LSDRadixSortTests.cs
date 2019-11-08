using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class LSDRadixSortTests : SortTestsBase
    {
        protected override IIntegerSortAlgorhythm GetIntAlgorhythm()
        {
            return new LSDRadixSort(4, new LocalSignSeparator());
        }
    }
}
