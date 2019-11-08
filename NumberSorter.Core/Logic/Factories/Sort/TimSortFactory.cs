﻿using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.Sort
{
    public abstract class TimSortFactory : GenericSortFactory
    {
        private ILocalMergeFactory LocalMergeFactory { get; }
        private IPartialSortFactory MinrunSortFactory { get; }

        protected TimSortFactory(ILocalMergeFactory localMergeFactory, IPartialSortFactory minrunSortFactory)
        {
            LocalMergeFactory = localMergeFactory;
            MinrunSortFactory = minrunSortFactory;
        }

        public override ISortAlgorhythm<T> GetSort<T>(IComparer<T> comparer)
        {
            return new TimSort<T>(comparer, LocalMergeFactory, MinrunSortFactory);
        }
    }
}