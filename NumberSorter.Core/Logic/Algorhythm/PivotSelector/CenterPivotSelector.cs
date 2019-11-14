using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PivotSelector
{
    public sealed class CenterPivotSelector<T> : GenericPivotSelector<T>
    {
        public CenterPivotSelector(IComparer<T> comparer) : base(comparer) { }

        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex)
        {
            return (firstIndex + lastIndex) / 2;
        }
    }
}
