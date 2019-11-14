﻿using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PivotSelector
{
    public sealed class LastPivotSelector<T> : GenericPivotSelector<T>
    {
        public LastPivotSelector(IComparer<T> comparer) : base(comparer) { }

        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex)
        {
            return lastIndex;
        }
    }
}
