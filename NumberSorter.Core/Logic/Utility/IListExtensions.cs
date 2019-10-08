using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Utility
{
    public static class IListExtensions
    {
        public static void Swap<T>(this IList<T> list, int first, int second)
        {
            T temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }

        public static T[] GetRangeAsArray<T>(this IList<T> list, int start, int length)
        {
            int firstIndex = start;
            int secondIndex = 0;
            var result = new T[length];

            int elementsToCopy = length;
            while (elementsToCopy-- > 0)
                result[secondIndex++] = list[firstIndex++];

            return result;
        }

        //public static int Compare<T>(this IList<T> list, int first, int second, IComparer<T> comparer)
        //{
        //    return comparer.Compare(list[first], list[second]);
        //}
    }
}
