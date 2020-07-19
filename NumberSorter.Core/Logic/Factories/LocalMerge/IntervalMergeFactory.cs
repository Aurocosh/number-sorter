using NumberSorter.Core.Logic.Algorhythm.LocalMerge;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class IntervalMergeFactory : ILocalMergeFactory
    {
        private IPositionLocatorFactory PositionLocatorFactory { get; }

        public IntervalMergeFactory(IPositionLocatorFactory positionLocatorFactory)
        {
            PositionLocatorFactory = positionLocatorFactory;
        }

        public ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer)
        {
            return new IntervalMerge<T>(comparer, PositionLocatorFactory);
        }
    }
}
