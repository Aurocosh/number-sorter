using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Logic.Algorhythm.QuickSort.PivotSelectors
{
    internal sealed class RandomPivotSelector<T> : QuickSortPivotSelector<T>
    {
        private readonly Random _random = new Random();

        public override int SelectPivot(IList<T> list, int firstIndex, int lastIndex, IComparer<T> comparer)
        {
            return _random.Next(firstIndex, lastIndex);
        }
    }
}
