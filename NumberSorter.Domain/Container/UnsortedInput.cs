using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Domain.Container
{
    public class UnsortedInput<T>
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Count => _values.Length;
        public IReadOnlyList<T> Values => _values;

        private readonly T[] _values;

        public UnsortedInput()
        {
            Name = "Empty";
            Id = Guid.Empty;
            _values = Array.Empty<T>();
        }

        public UnsortedInput(string name, IEnumerable<T> values)
        {
            Name = name;
            Id = Guid.NewGuid();
            _values = values.ToArray();
        }

        public UnsortedInput(string name, Guid id, IEnumerable<T> values)
        {
            Id = id;
            Name = name;
            _values = values.ToArray();
        }
    }
}
