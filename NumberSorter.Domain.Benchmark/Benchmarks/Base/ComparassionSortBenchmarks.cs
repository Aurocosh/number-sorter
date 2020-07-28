using NumberSorter.Core.Logic.Comparer;
using NumberSorter.Core.Logic.Factories.Sort.Base;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Base
{
    public abstract class ComparassionSortBenchmarks : SortBenchmarks
    {
        protected abstract ISortFactory GetSortFactory();

        protected ComparassionSortBenchmarks()
        {
        }

        protected override void Sort(int[] list)
        {
            var comparer = new IntComparer();
            var sortFactory = GetSortFactory();
            sortFactory.Sort(list, comparer);
        }
    }
}
