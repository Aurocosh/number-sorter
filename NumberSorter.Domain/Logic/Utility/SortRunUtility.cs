﻿using NumberSorter.Domain.Logic.Algorhythm;
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

        public static string RunToString<T>(IList<T> list, SortRun sortRun, int firstIndex, int secondIndex)
        {
            if (list is IList<int> intList)
            {
                var values = new int[sortRun.Length];
                var strings = new string[sortRun.Length];
                for (int i = 0; i < sortRun.Length; i++)
                {
                    var index = sortRun.Start + i;
                    var value = intList[index].ToString();

                    if (index == firstIndex && index == secondIndex)
                        value = "{" + value + "}";
                    else if (index == firstIndex)
                        value = "(" + value + ")";
                    else if (index == secondIndex)
                        value = "[" + value + "]";

                    strings[i] = value;
                }

                return string.Join(", ", strings.Select(x => x));
            }
            return "";
        }
    }
}