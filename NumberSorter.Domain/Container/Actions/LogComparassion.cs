using Newtonsoft.Json;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogComparassion<T> : LogAction<T> where T : IEquatable<T>
    {
        [JsonProperty]
        public T FirstValue { get; }
        [JsonProperty]
        public T SecondValue { get; }

        public override int ComparassionCount => 1;

        [JsonProperty]
        public override int ComparassionResult { get; }
        [JsonProperty]
        public override int FirstComparedIndex { get; }
        [JsonProperty]
        public override int SecondComparedIndex { get; }

        public override IReadOnlyList<T> HighlightedValues => new T[] { FirstValue, SecondValue };
        public override IReadOnlyList<int> HighlightedIndexes => new int[] { FirstComparedIndex, SecondComparedIndex };

        public LogComparassion(int actionIndex, int firstComparedIndex, int secondComparedIndex, T firstValue, T secondValue, int comparassionResult) : base(actionIndex, LogActionType.LogComparassion)
        {
            FirstComparedIndex = firstComparedIndex;
            SecondComparedIndex = secondComparedIndex;
            FirstValue = firstValue;
            SecondValue = secondValue;
            ComparassionResult = comparassionResult;
        }

        public override string ToString() => $"Element {FirstValue} ({FirstComparedIndex}) compared to {SecondValue} ({SecondComparedIndex}). Result: {ComparassionResult}.";
    }
}
