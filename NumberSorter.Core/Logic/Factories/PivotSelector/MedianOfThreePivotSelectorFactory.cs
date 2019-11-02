﻿using NumberSorter.Core.Logic.Algorhythm.PivotSelector;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PivotSelector
{
    public class MedianOfThreePivotSelectorFactory : IPivotSelectorFactory
    {
        public IPivotSelector<T> GetPivotSelector<T>(IComparer<T> comparer)
        {
            return new MedianThreePivotSelector<T>(comparer);
        }
    }
}
