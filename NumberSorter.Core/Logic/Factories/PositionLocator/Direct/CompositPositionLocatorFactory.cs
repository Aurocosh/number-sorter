using NumberSorter.Core.Logic.Algorhythm.PositionLocator;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PositionLocator
{
    public class CompositPositionLocatorFactory : IPositionLocatorFactory
    {
        private int LinearCount { get; }

        public CompositPositionLocatorFactory(int linearCount)
        {
            LinearCount = linearCount;
        }

        public IPositionLocator<T> GetPositionLocator<T>(IComparer<T> comparer)
        {
            return new CompositPositionLocator<T>(comparer, LinearCount);
        }
    }
}
