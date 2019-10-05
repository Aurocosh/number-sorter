using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Domain.Logic.Utility
{
    public static class IListUtility
    {
        private static readonly Random _random = new Random();

        public static void Randomize(IList<int> list, int minimum, int maximum)
        {
            int size = list.Count;
            for (int i = 0; i < size; i++)
                list[i] = _random.Next(minimum, maximum);
        }
    }
}
