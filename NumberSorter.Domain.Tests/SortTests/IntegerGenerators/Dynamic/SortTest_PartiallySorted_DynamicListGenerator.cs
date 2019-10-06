using NumberSorter.Domain.Generators;
using NumberSorter.Domain.Tests.IntegerGenerators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Dynamic
{
    public class SortTest_PartiallySorted_DynamicListGenerator : IEnumerable<object[]>
    {
        private static readonly RandomPartialSortedIntegerGenerator _generator = new RandomPartialSortedIntegerGenerator(TestsRandomProvider.Random);
        private static readonly List<object[]> _data;

        static SortTest_PartiallySorted_DynamicListGenerator()
        {
            var runCounts = new List<int> { 1, 2, 3, 10, 100 };
            var runSizeRanges = new List<Vector2Int> { new Vector2Int(1, 10), new Vector2Int(500, 1000) };

            var query =
                from runCount in runCounts
                from runSizeRange in runSizeRanges
                select new { runCount, runSizeRange };

            _data = new List<object[]>();

            var inputValues = query.ToList();
            for (int i = 0; i < 1; i++)
            {
                var arguments = inputValues
                    .Select(x => new object[] {
                        _generator.Generate(int.MinValue, int.MaxValue, x.runSizeRange.Min, x.runSizeRange.Max, x.runCount,0.5,0) });
                _data.AddRange(arguments);
            }
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}