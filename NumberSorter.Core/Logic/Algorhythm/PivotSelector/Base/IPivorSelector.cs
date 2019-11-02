using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PivotSelector
{
    public interface IPivotSelector<T> : IComparingAlgorhythm<T>
    {
        int SelectPivot(IList<T> list, int firstIndex, int lastIndex);
    }
}
