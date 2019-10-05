﻿using NumberSorter.Domain.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace NumberSorter.Domain.Tests
{
    public class SortTest_RandomUnsorted_DynamicListGenerator : IEnumerable<object[]>
    {
        private static readonly RandomIntegerGenerator _generator = new RandomIntegerGenerator();
        private static readonly List<object[]> _data;

        static SortTest_RandomUnsorted_DynamicListGenerator()
        {
            var arrayLengths = new List<int> { 8, 9, 10, 100, 1000 };

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