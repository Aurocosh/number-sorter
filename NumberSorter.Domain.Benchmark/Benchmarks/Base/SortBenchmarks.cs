using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Order;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Domain.Benchmark.IntegerGenerators;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Comparer;
using NumberSorter.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Base
{
    //[RPlotExporter]
    [CsvMeasurementsExporter]
    [Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
    public abstract class SortBenchmarks
    {
        static SortBenchmarks()
        {
        }

        private IComparer<int> Comparer { get; }
        private readonly ISortAlgorhythm<int> _sort;

        public SortBenchmarks()
        {
            Comparer = new IntComparer();
            _sort = GetAlgorhythm(Comparer);
        }

        public IEnumerable<object[]> TwoFullySortedParts_FirstBiggerThenSecond_DynamicList() => new SortBenchmark_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator().ToList();
        public IEnumerable<object[]> RandomUnsorted_DynamicList() => new SortBenchmark_RandomUnsorted_DynamicListGenerator().ToList();
        public IEnumerable<object[]> PartiallySorted_DynamicList() => new SortBenchmark_PartiallySorted_DynamicListGenerator().ToList();

        protected abstract ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer);

        [Benchmark]
        [ArgumentsSource(nameof(TwoFullySortedParts_FirstBiggerThenSecond_DynamicList))]
        public void TwoFullySortedParts_FirstBiggerThenSecond_Dynamic(int size, List<int> testData)
        {
            _sort.Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(RandomUnsorted_DynamicList))]
        public void RandomUnsorted_Dynamic(int size, List<int> testData)
        {
            _sort.Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(PartiallySorted_DynamicList))]
        public void PartiallySorted_Dynamic(int size, List<int> testData)
        {
            _sort.Sort(testData);
        }
    }
}
