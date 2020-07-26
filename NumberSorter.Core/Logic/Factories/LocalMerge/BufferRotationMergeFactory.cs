using NumberSorter.Core.Logic.Algorhythm.LocalMerge;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class BufferRotationMergeFactory : ILocalMergeFactory
    {
        private IPositionLocatorFactory PositionLocatorFactory { get; }
        private ILocalRotationFactory LocalRotationFactory { get; }

        public BufferRotationMergeFactory(IPositionLocatorFactory positionLocatorFactory, ILocalRotationFactory localRotationFactory)
        {
            PositionLocatorFactory = positionLocatorFactory;
            LocalRotationFactory = localRotationFactory;
        }

        public ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer, IList<T> list)
        {
            return new BufferRotationMerge<T>(comparer, PositionLocatorFactory, LocalRotationFactory, list);
        }
    }
}
