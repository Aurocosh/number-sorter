using NumberSorter.Core.Logic.Algorhythm.PositionLocator;
using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PositionLocator
{
    public class BiasedBinaryPositionLocatorFactory : IPositionLocatorFactory
    {
        private int IndexBias { get; }

        public BiasedBinaryPositionLocatorFactory(int indexBias)
        {
            IndexBias = indexBias;
        }

        public IPositionLocator<T> GetPositionLocator<T>(IComparer<T> comparer)
        {
            return new BiasedBinaryPositionLocator<T>(comparer, IndexBias);
        }
    }
}
