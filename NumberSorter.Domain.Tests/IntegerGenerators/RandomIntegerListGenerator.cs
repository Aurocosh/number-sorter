using NumberSorter.Domain.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Domain.Tests
{
    public class DynamicRandom100IntegerListGenerator : IEnumerable<object[]>
    {
        private static readonly RandomIntegerGenerator _generator = new RandomIntegerGenerator();

        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {_generator.Generate(-100,100,100)},
            new object[] {_generator.Generate(-100,100,100)},
            new object[] {_generator.Generate(-100,100,100)},
            new object[] {_generator.Generate(-100,100,100)},
            new object[] {_generator.Generate(-100,100,100)},

            new object[] {_generator.Generate(-100,100,100)},
            new object[] {_generator.Generate(-100,100,100)},
            new object[] {_generator.Generate(-100,100,100)},
            new object[] {_generator.Generate(-100,100,100)},
            new object[] {_generator.Generate(-100,100,100)},
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}