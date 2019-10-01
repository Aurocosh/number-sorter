using NumberSorter.Domain.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberSorter.Domain.Tests
{
    public class DynamicRandomIntegerGenerator : IEnumerable<object[]>
    {
        private static readonly RandomIntegerGenerator _generator = new RandomIntegerGenerator();
        private static readonly List<object[]> _data;

        static DynamicRandomIntegerGenerator()
        {
            var first = Enumerable.Range(0, 15)
                .Select(_ => _generator.Generate(-100, 100, 100))
                .Select(x => new object[] { x });
            var second = Enumerable.Range(0, 15)
                .Select(_ => _generator.Generate(int.MinValue, int.MaxValue, 100))
                .Select(x => new object[] { x });

            _data = first.Concat(second).ToList();
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}