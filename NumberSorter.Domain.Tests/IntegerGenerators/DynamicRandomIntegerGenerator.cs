using NumberSorter.Domain.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using static NumberSorter.Domain.Tests.TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator;

namespace NumberSorter.Domain.Tests
{
    public class RandomUnsorted_DynamicListGenerator : IEnumerable<object[]>
    {
        private static readonly RandomIntegerGenerator _generator = new RandomIntegerGenerator();
        private static readonly List<object[]> _data;

        static RandomUnsorted_DynamicListGenerator()
        {
            var arrayLengths = new List<int> { 8, 9, 10, 40, 60, 80, 100 };
            var valueRanges = new List<Vector2Int> { new Vector2Int(-100, 100), new Vector2Int(int.MinValue, int.MaxValue) };

            var query =
                from length in arrayLengths
                from range in valueRanges
                select new { length, range };

            _data = new List<object[]>();

            var inputValues = query.ToList();
            for (int i = 0; i < 1; i++)
            {
                var arguments = inputValues
                    .Select(x => new object[] {
                        _generator.Generate(x.range.Min, x.range.Max, x.length) });
                _data.AddRange(arguments);
            }
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}