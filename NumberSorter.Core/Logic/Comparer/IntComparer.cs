using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Comparer
{
    public class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }
}
