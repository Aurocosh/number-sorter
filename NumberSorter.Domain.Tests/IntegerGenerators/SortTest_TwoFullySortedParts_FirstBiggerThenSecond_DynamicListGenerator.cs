﻿using NumberSorter.Domain.Generators;
using NumberSorter.Domain.Logic.Algorhythm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace NumberSorter.Domain.Tests
{
    // Generates a very specific array of integers that consists of two fully sorted parts.
    // Also all elements from first part is bigger than any element from second part
    // For example:
    // 5,6,1,2,3
    // 12,14,15,20,1,3

    public partial class SortTest_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator : IEnumerable<object[]>
    {
        private static readonly UnmergedFullySortedGenerator _generator = new UnmergedFullySortedGenerator();
        private static readonly List<object[]> _data;

        static SortTest_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator()
        {
            var lengths = new List<int> { 1, 2, 3, 8, 9, 10, 100 };
            var valueRanges = new List<Vector2Int> { new Vector2Int(-100, 100), new Vector2Int(int.MinValue, int.MaxValue) };

            var query =
                from firstLength in lengths
                from SecondLength in lengths
                from range in valueRanges
                select new { firstLength, SecondLength, range };

            _data = new List<object[]>();

            var inputValues = query.ToList();
            for (int i = 0; i < 1; i++)
            {
                var arguments = inputValues
                    .Select(x => new object[] {
                        _generator.Generate(x.range.Min, x.range.Max, x.firstLength, x.SecondLength),
                        new SortRun(0, x.firstLength),
                        new SortRun(x.firstLength, x.SecondLength) });
                _data.AddRange(arguments);
            }
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}