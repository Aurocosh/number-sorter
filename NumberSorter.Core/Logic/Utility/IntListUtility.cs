using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Core.Logic.Utility
{
    public static class IntListUtility
    {
        public static void InvertNumbers(IList<int> list, int startingIndex, int length)
        {
            int indexLimit = startingIndex + length;
            for (int i = 0; i != indexLimit; i++)
                list[i] = -list[i];
        }
    }
}
