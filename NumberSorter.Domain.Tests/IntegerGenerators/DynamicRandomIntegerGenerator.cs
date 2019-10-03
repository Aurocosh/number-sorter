using NumberSorter.Domain.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace NumberSorter.Domain.Tests
{
    public class DynamicRandomIntegerGenerator : IEnumerable<object[]>
    {
        private struct Vector2Int
        {
            public int Min { get; }
            public int Max { get; }

            public Vector2Int(int min, int max)
            {
                Min = min;
                Max = max;
            }
        }
        private struct GeneratorInput
        {
            public Vector2Int Range { get; }
            public int Count { get; }

            public GeneratorInput(Vector2Int range, int count)
            {
                Range = range;
                Count = count;
            }
        }

        private static readonly RandomIntegerGenerator _generator = new RandomIntegerGenerator();
        private static readonly List<object[]> _data;

        static DynamicRandomIntegerGenerator()
        {
            var arrayLengths = new List<int> { 8, 9, 10, 40, 60, 80, 100 };
            var valueRanges = new List<Vector2Int> { new Vector2Int(-100, 100), new Vector2Int(int.MinValue, int.MaxValue) };

            var generatorSettings = arrayLengths.Join(valueRanges, _ => true, _ => true, (length, range) => new GeneratorInput(range, length)).ToList();

            _data = new List<object[]>();
            for (int i = 0; i < 10; i++)
            {
                var first = generatorSettings
                    .Select(x => _generator.Generate(x.Range.Min, x.Range.Max, x.Count))
                    .Select(x => new object[] { x });
                _data.AddRange(first);
            }
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}