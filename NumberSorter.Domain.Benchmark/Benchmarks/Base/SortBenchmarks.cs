using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Benchmark.IntegerGenerators;
using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Logic.Comparer;
using NumberSorter.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Base
{
    [RPlotExporter]
    [CsvMeasurementsExporter]
    public abstract class SortBenchmarks
    {
        static SortBenchmarks()
        {
        }

        private IComparer<int> _comparer;
        private readonly ISortAlgorhythm<int> _sort;

        public SortBenchmarks()
        {
            _comparer = new IntComparer();
            _sort = GetAlgorhythm(_comparer);
        }

        public IEnumerable<object> TwoFullySortedParts_FirstBiggerThenSecond_DynamicList() => new SortBenchmark_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator().GetEnumerable().Select(x => x[0]).ToList();
        public IEnumerable<object> RandomUnsorted_DynamicList() => new SortBenchmark_RandomUnsorted_DynamicListGenerator().GetEnumerable().Select(x => x[0]).ToList();

        protected abstract ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer);

        [Benchmark]
        [ArgumentsSource(nameof(TwoFullySortedParts_FirstBiggerThenSecond_DynamicList))]
        public void TwoFullySortedParts_FirstBiggerThenSecond_Dynamic(List<int> testData)
        {
            _sort.Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(RandomUnsorted_DynamicList))]
        public void RandomUnsorted_Dynamic(List<int> testData)
        {
            _sort.Sort(testData);
        }
    }
}
