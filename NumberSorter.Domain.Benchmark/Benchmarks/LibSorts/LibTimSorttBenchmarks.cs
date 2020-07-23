using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using System.Linq;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class LibTimSorttBenchmarks : SortBenchmarks
    {
        private sealed class LibTimSort<T> : GenericSortAlgorhythm<T>
        {
            public LibTimSort(IComparer<T> comparer) : base(comparer) { }

            public override void Sort(IList<T> list, int firstIndex, int length)
            {
                list.TimSort(firstIndex, length);
            }
        }

        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new LibTimSort<int>(comparer);
        }
    }
}
