using NumberSorter.Domain.Generators;
using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Tests.IntegerGenerators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Dynamic
{
    // Generates a very specific array of integers that consists of two fully sorted parts.
    // Also all elements from first part is bigger than any element from second part
    // For example:
    // 5,6,1,2,3
    // 12,14,15,20,1,3

    public partial class SortTest_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator : IEnumerable<object[]>
    {
        private static readonly UnmergedFullySortedGenerator _generator = new UnmergedFullySortedGenerator(TestsRandomProvider.Random);
        private static readonly List<object[]> _data;

        static SortTest_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator()
        {
            var lengths = new List<int> { 1, 2, 3, 10, 1000 };

            var query =
                from firstLength in lengths
                from SecondLength in lengths
                select new { firstLength, SecondLength };

            _data = new List<object[]>();

            var inputValues = query.ToList();
            for (int i = 0; i < 1; i++)
            {
                var arguments = inputValues
                    .Select(x => new object[] {
                        _generator.Generate(int.MinValue, int.MaxValue, x.firstLength, x.SecondLength)});
                _data.AddRange(arguments);
            }
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}