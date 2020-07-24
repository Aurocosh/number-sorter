﻿using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public class StrandInPlaceSortFactory : GenericSortFactory
    {
        private ILocalMergeFactory LocalMergeFactory { get; }

        public StrandInPlaceSortFactory(ILocalMergeFactory localMergeFactory)
        {
            LocalMergeFactory = localMergeFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new StrandInPlaceSort<T>(comparer, LocalMergeFactory);
        }
    }
}
