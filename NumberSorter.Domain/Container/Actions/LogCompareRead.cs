using Newtonsoft.Json;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogCompareRead<T> : LogAction<T> where T : IEquatable<T>
    {
        [JsonProperty]
        public T Value { get; }

        public override int ReadCount => 1;
        [JsonProperty]
        public override int ReadIndex { get; }

        public override IReadOnlyList<T> HighlightedValues => new T[] { Value };
        public override IReadOnlyList<int> HighlightedIndexes => new int[] { ReadIndex };

        public LogCompareRead(int actionIndex, int readIndex, T value) : base(actionIndex, LogActionType.LogCompareRead)
        {
            Value = value;
            ReadIndex = readIndex;
        }

        public override string ToString() => $"Value {Value} read from index {ReadIndex}.";
    }
}
