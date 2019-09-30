using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Logic.Container
{
    public static class IListExtensions
    {
        public static void Swap<T>(this IList<T> list, int first, int second)
        {
            T temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }

        //public static int Compare<T>(this IList<T> list, int first, int second, IComparer<T> comparer)
        //{
        //    return comparer.Compare(list[first], list[second]);
        //}
    }
}
