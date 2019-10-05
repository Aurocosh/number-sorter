using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class KindaInPlaceMergeSortTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new KindaInPlaceMergeSort<int>(comparer);
        }
    }
}
