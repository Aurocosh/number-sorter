using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.PivotSelector
{
    public sealed class RandomPivotSelector<T> : GenericPivotSelector<T>
    {
        private Random Random { get; }

        public RandomPivotSelector(IComparer<T> comparer, Random random) : base(comparer)
        {
            Random = random;
        }

        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex)
        {
            return Random.Next(firstIndex, lastIndex);
        }
    }
}
