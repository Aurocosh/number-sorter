using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class LogAction<T> where T : IEquatable<T>
    {
        [JsonProperty]
        public int ActionIndex { get; }

        [JsonProperty]
        public LogActionType ActionType { get; }

        public virtual int ReadIndex => -1;

        public virtual int FirstWrittenIndex => -1;
        public virtual int SecondtWrittenIndex => -1;

        public virtual int ComparassionResult => 0;
        public virtual int FirstComparedIndex => -1;
        public virtual int SecondComparedIndex => -1;

        public virtual int ReadCount => 0;
        public virtual int WriteCount => 0;
        public virtual int ComparassionCount => 0;

        public virtual IReadOnlyList<T> HighlightedValues => Array.Empty<T>();
        public virtual IReadOnlyList<int> HighlightedIndexes => Array.Empty<int>();

        protected LogAction(int actionIndex, LogActionType actionType)
        {
            ActionIndex = actionIndex;
            ActionType = actionType;
        }

        public virtual void TransformStateArray(T[] stateArray) { }
    }
}
