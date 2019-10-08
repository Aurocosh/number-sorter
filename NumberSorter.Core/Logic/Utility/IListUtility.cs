using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Core.Logic.Utility
{
    public static class IListUtility
    {
        public static void Randomize(IList<int> list, int minimum, int maximum, Random random)
        {
            int size = list.Count;
            for (int i = 0; i < size; i++)
                list[i] = random.Next(minimum, maximum);
        }

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

        public static bool IsSorted<T>(IReadOnlyList<T> list, IComparer<T> comparer)
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
