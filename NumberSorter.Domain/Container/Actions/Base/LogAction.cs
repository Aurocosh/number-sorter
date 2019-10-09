using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions.Base
{
    public class LogAction<T> where T : IEquatable<T>
    {
        public int ActionIndex { get; }

        public LogAction(int actionIndex)
        {
            ActionIndex = actionIndex;
        }
    }
}
