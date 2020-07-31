using NumberSorter.Core.Logic.Algorhythm.LocalMerge;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class RelativeMergeFactory : ILocalMergeFactory
    {
        private IPositionLocatorFactory PositionLocatorFactory { get; }
        private IPositionLocatorFactory InversePositionLocatorFactory { get; }

        public RelativeMergeFactory(IPositionLocatorFactory positionLocatorFactory, IPositionLocatorFactory inversePositionLocatorFactory)
        {
            PositionLocatorFactory = positionLocatorFactory;
            InversePositionLocatorFactory = inversePositionLocatorFactory;
        }

        public ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer, IList<T> list)
        {
            return new RelativeMerge<T>(comparer, PositionLocatorFactory, InversePositionLocatorFactory, list);
        }
    }
}
