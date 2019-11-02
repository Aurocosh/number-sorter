using NumberSorter.Core.Logic.Algorhythm.PivotSelector;
using NumberSorter.Core.Logic.Factories.PivotSelector.Base;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Factories.PivotSelector
{
    public class RandomPivotSelectorFactory : IPivotSelectorFactory
    {
        private Random Random { get; }

        public RandomPivotSelectorFactory(Random random)
        {
            Random = random;
        }

        public IPivotSelector<T> GetPivotSelector<T>(IComparer<T> comparer)
        {
            return new RandomPivotSelector<T>(comparer, Random);
        }
    }
}
