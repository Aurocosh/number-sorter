using BenchmarkDotNet.Attributes;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Logic.Comparer;
using NumberSorter.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Benchmark
{
    [RPlotExporter]
    public abstract class SortBenchmarks
    {
        private static List<object> _staticRandom100IntegerList;
        private static List<object> _dynamicRandom100IntegerList;

        static SortBenchmarks()
        {
            _staticRandom100IntegerList = new StaticRandom100IntegerListGenerator().GetEnumerable().Select(x => x[0]).ToList();
            _dynamicRandom100IntegerList = new DynamicRandom100IntegerListGenerator().GetEnumerable().Select(x => x[0]).ToList();
        }

        private IComparer<int> _comparer;
        private readonly ISortAlgorhythm<int> _sort;

        public SortBenchmarks()
        {
            _comparer = new IntComparer();
            _sort = GetAlgorhythm(_comparer);
        }

        public IEnumerable<object> StaticRandom100IntegerList() => _staticRandom100IntegerList;
        public IEnumerable<object> DynamicRandom100IntegerList() => _dynamicRandom100IntegerList;

        protected abstract ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer);

        [Benchmark]
        [ArgumentsSource(nameof(StaticRandom100IntegerList))]
        public void TestSort100RandomStatic(List<int> testData)
        {
            _sort.Sort(testData);
        }

        [Benchmark]
        [ArgumentsSource(nameof(DynamicRandom100IntegerList))]
        public void TestSort100RandomDynamic(List<int> testData)
        {
            _sort.Sort(testData);
        }
    }
}
