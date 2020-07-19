using System;

namespace NumberSorter.Domain.Container
{
    public class ValueWrite<T> where T : IEquatable<T>
    {
        public int Index { get; }
        public T WrittenValue { get; }
        public T ReplacedValue { get; }

        public ValueWrite(int index, T writtenValue, T replacedValue)
        {
            Index = index;
            WrittenValue = writtenValue;
            ReplacedValue = replacedValue;
        }
    }
}
