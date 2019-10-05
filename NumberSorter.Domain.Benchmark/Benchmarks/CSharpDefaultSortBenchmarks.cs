using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Domain.Logic.Algorhythm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class CSharpDefaultSortBenchmarks : SortBenchmarks
    {
        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new CSharpDefaultSort<int>(comparer);
        }
    }
}
