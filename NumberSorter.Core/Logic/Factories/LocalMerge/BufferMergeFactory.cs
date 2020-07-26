using NumberSorter.Core.Logic.Algorhythm.LocalMerge;
using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.LocalMerge
{
    public class BufferMergeFactory : ILocalMergeFactory
    {
        private IPositionLocatorFactory PositionLocatorFactory { get; }

        public BufferMergeFactory(IPositionLocatorFactory positionLocatorFactory)
        {
            PositionLocatorFactory = positionLocatorFactory;
        }

        public ILocalMergeAlgothythm<T> GetLocalMerge<T>(IComparer<T> comparer, IList<T> list)
        {
            return new BufferMerge<T>(comparer, PositionLocatorFactory, list);
        }
    }
}
