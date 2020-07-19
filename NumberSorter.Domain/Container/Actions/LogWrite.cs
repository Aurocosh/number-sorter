using Newtonsoft.Json;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogWrite<T> : LogAction<T> where T : IEquatable<T>
    {
        [JsonProperty]
        public T Value { get; }

        public override int WriteCount => 1;
        [JsonProperty]
        public override int FirstWrittenIndex { get; }

        public override IReadOnlyList<T> HighlightedValues => new T[] { Value };
        public override IReadOnlyList<int> HighlightedIndexes => new int[] { FirstWrittenIndex };

        public LogWrite(int actionIndex, int index, T value) : base(actionIndex, LogActionType.LogWrite)
        {
            FirstWrittenIndex = index;
            Value = value;
        }

        public override string ToString() => $"Value {Value} written to index {FirstWrittenIndex}.";

        public override void TransformStateArray(T[] stateArray)
        {
            stateArray[FirstWrittenIndex] = Value;
        }
    }
}
