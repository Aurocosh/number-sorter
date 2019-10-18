using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Container
{
    public class UnsortedInput<T>
    {
        public Guid Id { get; }
        public int Count => _values.Length;
        public IReadOnlyList<T> Values => _values;

        private readonly T[] _values;

        public UnsortedInput()
        {
            Id = Guid.Empty;
            _values = Array.Empty<T>();
        }

        public UnsortedInput(IEnumerable<T> values)
        {
            Id = Guid.NewGuid();
            _values = values.ToArray();
        }

        public UnsortedInput(Guid id, IEnumerable<T> values)
        {
            Id = id;
            _values = values.ToArray();
        }
    }
}
