using NumberSorter.Domain.Benchmark.IntegerGenerators;
using NumberSorter.Domain.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace NumberSorter.Domain.Tests
{
    public class SortBenchmark_PartiallySorted_DynamicListGenerator : IEnumerable<object[]>
    {
        private static readonly RandomPartialSortedIntegerGenerator _generator = new RandomPartialSortedIntegerGenerator(BenchmarkRandomProvider.Random);
        private static readonly List<object[]> _data;

        static SortBenchmark_PartiallySorted_DynamicListGenerator()
        {
            var runCounts = new List<int> { 2, 10, 100 };
            var runSizeRanges = new List<Vector2Int> { new Vector2Int(2, 10), new Vector2Int(8000, 10000) };

            var query =
                from runCount in runCounts
                from runSizeRange in runSizeRanges
                select new { runCount, runSizeRange };

            _data = new List<object[]>();

            var inputValues = query.ToList();
            for (int i = 0; i < 1; i++)
            {
                var arguments = inputValues
                    .Select(x => new { input = x, list = _generator.Generate(int.MinValue, int.MaxValue, x.runSizeRange.Min, x.runSizeRange.Max, x.runCount, 0.5, 0) })
                    .Select(x => new object[] { x.list.Count, x.list });

                _data.AddRange(arguments);
            }
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}