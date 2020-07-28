using NumberSorter.Core.Algorhythm;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Base
{
    public abstract class IntegerSortBenchmarks : SortBenchmarks
    {
        protected abstract IIntegerSortAlgorhythm GetSortAlgorithm();

        protected IntegerSortBenchmarks()
        {
        }

        protected override void Sort(int[] list)
        {
            var sortFactory = GetSortAlgorithm();
            sortFactory.Sort(list);
        }
    }
}
