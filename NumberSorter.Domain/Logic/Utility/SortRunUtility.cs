using NumberSorter.Domain.Logic.Algorhythm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberSorter.Domain.Logic.Utility
{
    public static class SortRunUtility
    {
        public static string RunToString<T>(IList<T> list, SortRun sortRun)
        {
            if (list is IList<int> intList)
            {
                var values = new int[sortRun.Length];
                for (int i = 0; i < sortRun.Length; i++)
                    values[i] = intList[sortRun.Start + i];
                return string.Join(", ", values.Select(x => x.ToString()));
            }
            return "";
        }
    }
}
