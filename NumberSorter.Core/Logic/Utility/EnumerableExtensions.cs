using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Utility
{
    public static class EnumerableExtensions
    {
        public static T MinOrDefault<T>(this IEnumerable<T> sequence)
        {
            return sequence.Any() ? sequence.Min() : default;
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> sequence)
        {
            return sequence.Any() ? sequence.Max() : default;
        }
    }
}
