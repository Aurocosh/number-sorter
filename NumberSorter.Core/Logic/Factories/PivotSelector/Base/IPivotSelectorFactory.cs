using NumberSorter.Core.Logic.Algorhythm.PivotSelector;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PivotSelector.Base
{
    public interface IPivotSelectorFactory
    {
        IPivotSelector<T> GetPivotSelector<T>(IComparer<T> comparer);
    }
}
