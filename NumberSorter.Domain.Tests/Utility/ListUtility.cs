using NumberSorter.Core.Logic.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Domain.Tests
{
    public static class ListUtility
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

        public static bool IsSorted<T>(IList<T> list, SortRun sortRun, IComparer<T> comparer)
        {
            int limit = sortRun.LastIndex;
            for (int i = sortRun.FirstIndex; i < limit; i++)
            {
                var first = list[i];
                var second = list[i + 1];

                int comparassion = comparer.Compare(first, second);
                if (comparassion > 0)
                    return false;
            }
            return true;
        }

        public static bool IsSortedValuesValid<T>(IList<T> input, IList<T> result, IComparer<T> comparer)
        {
            var sorted = new List<T>(input);
            sorted.Sort(comparer);

            for (int i = 0; i < result.Count; i++)
            {
                var first = sorted[i];
                var second = result[i];

                int comparassion = comparer.Compare(first, second);
                if (comparassion != 0)
                    return false;
            }

            return true;
        }
    }
}
