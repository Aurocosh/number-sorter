using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Domain.Tests
{
    internal static class ListUtility
    {
        public static bool IsSorted<T>(IList<T> list, IComparer<T> comparer)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var first = list[i];
                var second = list[i + 1];

                int comparassion = comparer.Compare(first, second);
                if (comparassion > 0)
                    return false;
            }
            return true;
        }
    }
}
