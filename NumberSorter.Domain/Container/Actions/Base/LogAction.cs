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

        public LogAction(int actionIndex)
        {
            ActionIndex = actionIndex;
        }

        public abstract SortState<T> TransformState(SortState<T> state);
    }
}
