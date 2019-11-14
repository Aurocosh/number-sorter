using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PivotSelector
{
    public sealed class FirstPivotSelector<T> : GenericPivotSelector<T>
    {
        public FirstPivotSelector(IComparer<T> comparer) : base(comparer) { }

        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex)
        {
            return firstIndex;
        }
    }
}
