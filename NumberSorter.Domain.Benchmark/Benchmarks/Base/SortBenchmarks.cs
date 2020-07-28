using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Order;
using NumberSorter.Domain.Benchmark.IntegerGenerators;
using System.Collections.Generic;
using NumberSorter.Core.Generators;
using NumberSorter.Core.CustomGenerators.Context;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Base
{
    [MemoryDiagnoser]
    //[RPlotExporter]
    [HtmlExporter]
    [JsonExporterAttribute.Brief]
    [Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
    public abstract class SortBenchmarks
    {
        private readonly BenchmarkDataManager _dataManager;

        protected SortBenchmarks()
        {
            _dataManager = new BenchmarkDataManager();

            var randomGenerator = new RandomIntegerGenerator(BenchmarkRandomProvider.Random);

            var randomUnsorted100 = randomGenerator.Generate(int.MinValue, int.MaxValue, 1000).ToArray();
            _dataManager.AddDataSet("Random unsorted (100)", randomUnsorted100);

            var randomUnsorted10000 = randomGenerator.Generate(int.MinValue, int.MaxValue, 1000).ToArray();
            _dataManager.AddDataSet("Random unsorted (10000)", randomUnsorted10000);

            // Templated int lists
            var context = new CustomConverterContext(BenchmarkRandomProvider.Random);
            var templateGenerator = new TemplateListGenerator(context);

            // Fully sorted
            var fullySorted100 = templateGenerator.Generate("FullySorted.FullySorted100");
            _dataManager.AddDataSet("Fully sorted (100)", fullySorted100);

            var fullySorted10000 = templateGenerator.Generate("FullySorted.FullySorted10000");
            _dataManager.AddDataSet("Fully sorted (10000)", fullySorted10000);

            // Almost sorted
            var almostSorted100 = templateGenerator.Generate("AlmostSorted.AlmostSorted100");
            _dataManager.AddDataSet("Almost sorted (100)", almostSorted100);

            var almostSorted10000 = templateGenerator.Generate("AlmostSorted.AlmostSorted10000");
            _dataManager.AddDataSet("Almost sorted (10000)", almostSorted10000);

            // Few unique
            var fewUnique100 = templateGenerator.Generate("FewUnique.FewUnique100");
            _dataManager.AddDataSet("Few unique (100)", fewUnique100);

            var fewUnique10000 = templateGenerator.Generate("FewUnique.FewUnique10000");
            _dataManager.AddDataSet("Few unique (10000)", fewUnique10000);

            // Inverted
            var inverted100 = templateGenerator.Generate("Inverted.Inverted100");
            _dataManager.AddDataSet("Inverted (100)", inverted100);

            var inverted10000 = templateGenerator.Generate("Inverted.Inverted10000");
            _dataManager.AddDataSet("Inverted (10000)", inverted10000);

            // Intervals
            var intervals100 = templateGenerator.Generate("Intervals.Intervals100");
            _dataManager.AddDataSet("Intervals (100)", intervals100);

            var intervals10000 = templateGenerator.Generate("Intervals.Intervals10000");
            _dataManager.AddDataSet("Intervals (10000)", intervals10000);

            // Shuffled intervals
            var shuffledIntervals100 = templateGenerator.Generate("ShuffledIntervals.ShuffledIntervals100");
            _dataManager.AddDataSet("Shuffled intervals (100)", shuffledIntervals100);

            var shuffledIntervals10000 = templateGenerator.Generate("ShuffledIntervals.ShuffledIntervals10000");
            _dataManager.AddDataSet("Shuffled intervals (10000)", shuffledIntervals10000);
        }

        public IEnumerable<object[]> SortParameters => _dataManager.GenerateParameters();

        [IterationSetup]
        public void IterationSetup()
        {
            _dataManager.Refresh();
        }

        [Benchmark]
        [ArgumentsSource(nameof(SortParameters))]
        public void Sort(string name, int index)
        {
            var list = _dataManager.GetDataSet(index);
            Sort(list);
        }

        protected abstract void Sort(int[] list);
    }
}
