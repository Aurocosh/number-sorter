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
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Base
{
    //[RPlotExporter]
    //[CsvMeasurementsExporter]
    //[XmlExporterAttribute.Brief]
    [Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
    public abstract class SortBenchmarks
    {
        static SortBenchmarks()
        {
        }

        private IComparer<int> Comparer { get; }
        private readonly ISortAlgorhythm<int> _sort;
        private readonly IIntegerSortAlgorhythm _integerSort;

        public SortBenchmarks()
        {
            Comparer = new IntComparer();
            _sort = GetAlgorhythm(Comparer);
            _integerSort = GetIntAlgorhythm();
        }

        public IEnumerable<object[]> Custom_Inverted_DynamicList() => new SortBenchmark_Custom_DynamicListGenerator("Inverted").ToList();
        public IEnumerable<object[]> Custom_Intervals_DynamicList() => new SortBenchmark_Custom_DynamicListGenerator("Intervals").ToList();
        public IEnumerable<object[]> Custom_FewUnique_DynamicList() => new SortBenchmark_Custom_DynamicListGenerator("FewUnique").ToList();
        public IEnumerable<object[]> Custom_FullySorted_DynamicList() => new SortBenchmark_Custom_DynamicListGenerator("FullySorted").ToList();
        public IEnumerable<object[]> Custom_AlmostSorted_DynamicList() => new SortBenchmark_Custom_DynamicListGenerator("AlmostSorted").ToList();
        public IEnumerable<object[]> Custom_ShuffledIntervals_DynamicList() => new SortBenchmark_Custom_DynamicListGenerator("ShuffledIntervals").ToList();
        public IEnumerable<object[]> TwoFullySortedParts_FirstBiggerThenSecond_DynamicList() => new SortBenchmark_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator().ToList();
        public IEnumerable<object[]> RandomUnsorted_DynamicList() => new SortBenchmark_RandomUnsorted_DynamicListGenerator().ToList();
        //public IEnumerable<object[]> PartiallySorted_DynamicList() => new SortBenchmark_PartiallySorted_DynamicListGenerator().ToList();

        protected virtual ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer) => null;
        protected virtual IIntegerSortAlgorhythm GetIntAlgorhythm() => null;

        [Benchmark]
        [ArgumentsSource(nameof(Custom_Inverted_DynamicList))]
        public void Custom_Inverted_Dynamic(int size, IList<int> testData)
        {
            Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Custom_FewUnique_DynamicList))]
        public void Custom_FewUnique_Dynamic(int size, IList<int> testData)
        {
            Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Custom_FullySorted_DynamicList))]
        public void Custom_FullySorted_Dynamic(int size, IList<int> testData)
        {
            Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Custom_AlmostSorted_DynamicList))]
        public void Custom_AlmostSorted_Dynamic(int size, IList<int> testData)
        {
            Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Custom_Intervals_DynamicList))]
        public void Custom_Intervals_Dynamic(int size, IList<int> testData)
        {
            Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Custom_ShuffledIntervals_DynamicList))]
        public void Custom_ShuffledIntervals_Dynamic(int size, IList<int> testData)
        {
            Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(TwoFullySortedParts_FirstBiggerThenSecond_DynamicList))]
        public void TwoFullySortedParts_FirstBiggerThenSecond_Dynamic(int size, List<int> testData)
        {
            Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(RandomUnsorted_DynamicList))]
        public void RandomUnsorted_Dynamic(int size, List<int> testData)
        {
            Sort(testData);
        }

        private void Sort(IList<int> list)
        {
            if (_integerSort != null)
                _integerSort.Sort(list);
            else
                _sort.Sort(list);
        }

        //[Benchmark]
        //[ArgumentsSource(nameof(PartiallySorted_DynamicList))]
        //public void PartiallySorted_Dynamic(int size, string name, List<int> testData)
        //{
        //    _sort.Sort(testData);
        //}
    }
}
