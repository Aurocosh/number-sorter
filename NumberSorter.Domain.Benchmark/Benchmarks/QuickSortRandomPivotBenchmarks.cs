using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Benchmark;
using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm.QuickSort.PivotSelectors;

namespace NumberSorter.Domain.Tests.SortTests
{
    public class QuickSortRandomPivotBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            var pivotSelector = new RandomPivotSelector<int>();
            return new QuickSort<int>(comparer, pivotSelector);
        }
    }
}
