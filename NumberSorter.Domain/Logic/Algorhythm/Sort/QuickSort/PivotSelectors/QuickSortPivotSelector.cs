﻿using System.Collections.Generic;

namespace NumberSorter.Domain.Logic.Algorhythm.QuickSort
{
    abstract public class QuickSortPivotSelector<T>
    {
        abstract public int SelectPivot(IList<T> list, int firstIndex, int lastIndex, IComparer<T> comparer);
    }
}