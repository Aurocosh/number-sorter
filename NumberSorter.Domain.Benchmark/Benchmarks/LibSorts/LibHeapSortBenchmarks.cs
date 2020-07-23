using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using System.Linq;
using Orc.Sort.NSort;
using NumberSorter.Core.Logic.Comparer;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class LibHeapSortBenchmarks : SortBenchmarks
    {
        private sealed class LibHeapSort<T> : GenericSortAlgorhythm<T>
        {
            public LibHeapSort(IComparer<T> comparer) : base(comparer) { }

            public override void Sort(IList<T> list, int firstIndex, int length)
            {
                var comparer = new IntComparer();
                var swap = new DefaultSwap();
                var sort = new HeapSort(comparer, swap);
                sort.Sort((System.Collections.IList)list);
            }
        }

        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new LibHeapSort<int>(comparer);
        }
    }
}
