using NumberSorter.Core.Logic.Algorhythm.PositionLocator;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PositionLocator
{
    public class ReversedBiasedBinaryPositionLocatorFactory : IPositionLocatorFactory
    {
        private int IndexBias { get; }

        public ReversedBiasedBinaryPositionLocatorFactory(int indexBias)
        {
            IndexBias = indexBias;
        }

        public IPositionLocator<T> GetPositionLocator<T>(IComparer<T> comparer)
        {
            return new ReversedBiasedBinaryPositionLocator<T>(comparer, IndexBias);
        }
    }
}
