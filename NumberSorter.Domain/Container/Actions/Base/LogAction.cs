using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions.Base
{
    public abstract class LogAction<T> where T : IEquatable<T>
    {
        public int ActionIndex { get; }
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

        protected LogAction(int actionIndex, LogActionType actionType)
        {
            ActionIndex = actionIndex;
            ActionType = actionType;
        }

        public virtual void TransformStateArray(T[] stateArray) { }
    }
}
