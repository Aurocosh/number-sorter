using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Domain.Logic.Utility
{
    public static class IListUtility
    {
        public static void Randomize(IList<int> list, int minimum, int maximum, Random random)
        {
            int size = list.Count;
            for (int i = 0; i < size; i++)
                list[i] = random.Next(minimum, maximum);
        }
    }
}
