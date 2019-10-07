using NumberSorter.Core.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Dynamic
{
    public class SortTest_RandomUnsorted_DynamicListGenerator : IEnumerable<object[]>
    {
        private static readonly RandomIntegerGenerator _generator = new RandomIntegerGenerator(TestsRandomProvider.Random);
        private static readonly List<object[]> _data;

        static SortTest_RandomUnsorted_DynamicListGenerator()
        {
            var arrayLengths = new List<int> { 1, 2, 3, 8, 9, 30, 50, 100, 1000, 2500 };

            var query =
                from length in arrayLengths
                select new { length };

            _data = new List<object[]>();

            var inputValues = query.ToList();
            for (int i = 0; i < 1; i++)
            {
                var arguments = inputValues
                    .Select(x => new object[] {
                        _generator.Generate(int.MinValue, int.MaxValue, x.length) });
                _data.AddRange(arguments);
            }
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}