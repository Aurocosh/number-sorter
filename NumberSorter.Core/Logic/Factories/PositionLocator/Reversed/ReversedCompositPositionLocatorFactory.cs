using NumberSorter.Core.Logic.Algorhythm.PositionLocator;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PositionLocator
{
    public class ReversedCompositPositionLocatorFactory : IPositionLocatorFactory
    {
        private int LinearCount { get; }

        public ReversedCompositPositionLocatorFactory(int linearCount)
        {
            LinearCount = linearCount;
        }

        public IPositionLocator<T> GetPositionLocator<T>(IComparer<T> comparer)
        {
            return new ReversedCompositPositionLocator<T>(comparer, LinearCount);
        }
    }
}
