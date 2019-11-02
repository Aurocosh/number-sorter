using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PositionLocator.Base
{
    public interface IPositionLocatorFactory
    {
        IPositionLocator<T> GetPositionLocator<T>(IComparer<T> comparer);
    }
}
