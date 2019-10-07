using NumberSorter.Core.Generators;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Benchmark.IntegerGenerators
{
    public class SortBenchmark_RandomUnsorted_DynamicListGenerator : IEnumerable<object[]>
    {
        private static readonly RandomIntegerGenerator _generator = new RandomIntegerGenerator(BenchmarkRandomProvider.Random);
        private static readonly List<object[]> _data;

        static SortBenchmark_RandomUnsorted_DynamicListGenerator()
        {
            var arrayLengths = new List<int> { 10, 100, 1000, 10000, 1000000 };

            var query =
                from length in arrayLengths
                select new { length };

            _data = new List<object[]>();

            var inputValues = query.ToList();
            for (int i = 0; i < 1; i++)
            {
                var arguments = inputValues
                    .Select(x => new { input = x, list = _generator.Generate(int.MinValue, int.MaxValue, x.length) })
                    .Select(x => new object[] { x.list.Count, x.list });
                _data.AddRange(arguments);
            }
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}