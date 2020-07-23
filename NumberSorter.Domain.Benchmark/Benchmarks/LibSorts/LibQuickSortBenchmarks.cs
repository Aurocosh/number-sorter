using System.Collections.Generic;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.Benchmarks.Base;
using NumberSorter.Core.Logic.Algorhythm;
using System.Linq;
using Orc.Sort.NSort;
using NumberSorter.Core.Logic.Comparer;
using Anoprsst;
using System;

namespace NumberSorter.Domain.Benchmark.Benchmarks
{
    public class LibQuickSortBenchmarks : SortBenchmarks
    {
        private sealed class Ord<T> : IOrdering<int>
        {
            public bool LessThan(int a, int b)
            {
                return a < b;
            }
        }

        private sealed class LibSort<T> : GenericSortAlgorhythm<T>
        {
            public LibSort(IComparer<T> comparer) : base(comparer) { }

            public override void Sort(IList<T> list, int firstIndex, int length)
            {
                var comparer = new IntComparer();
                var swap = new DefaultSwap();
                var sort = new ShellSort(comparer, swap);

                var arr = (T[])list;

                var span = arr.AsSpan();
                span.WithOrder(new Ord()).Sort();

                sort.Sort((System.Collections.IList)list);
            }
        }

        protected override ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new LibSort<int>(comparer);
        }
    }
}
