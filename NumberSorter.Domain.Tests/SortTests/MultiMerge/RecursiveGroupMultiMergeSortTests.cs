using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SortRunLocator;
using NumberSorter.Domain.Tests.SortTests.Base;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class RecursiveGroupMultiMergeSortTests : SortTestsBase
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new RecursiveMultiMergeSort<int>(comparer, new GroupingRunLocator<int>(comparer, new InsertionSort<int>(comparer), 32), x => new GroupingRunLocator<SortRun>(x, new InsertionSort<SortRun>(x), 32));

        }
    }
}
