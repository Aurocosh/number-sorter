using NumberSorter.Core.Logic.Algorhythm;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Utility
{
    public static class SortRunUtility
    {
        public static ArrayRunHalves SplitSortRun(SortRun sortRun)
        {
            if (sortRun.Length == 0)
                return new ArrayRunHalves(sortRun, sortRun);
            if (sortRun.Length == 1)
                return new ArrayRunHalves(sortRun, new SortRun(0, 0));

            int firstIndex = sortRun.Start;
            int firstLength = sortRun.Length / 2;

            int secondIndex = sortRun.Start + firstLength;
            int secondLength = sortRun.Length - firstLength;

            var firstSortRun = new SortRun(firstIndex, firstLength);
            var secondSortRun = new SortRun(secondIndex, secondLength);
            return new ArrayRunHalves(firstSortRun, secondSortRun);
        }

        public static void InvertRun<T>(IList<T> list, SortRun sortRun)
        {
            int leftIndex = sortRun.Start;
            int rightIndex = sortRun.Start + sortRun.Length - 1;
            int swapCount = sortRun.Length / 2;
            for (int i = 0; i < swapCount; i++)
                list.Swap(leftIndex++, rightIndex--);
        }

        public static string RunToString<T>(IList<T> list, SortRun sortRun)
        {
            if (sortRun.Length < 0)
                return "";
            var values = new T[sortRun.Length];
            for (int i = 0; i < sortRun.Length; i++)
                values[i] = list[sortRun.Start + i];
            return string.Join(", ", values.Select(x => x.ToString()));
        }

        public static string RunToString<T>(IList<T> list, SortRun sortRun, int firstIndex, int secondIndex, int firstSource)
        {
            var values = new T[sortRun.Length];
            var strings = new string[sortRun.Length];
            for (int i = 0; i < sortRun.Length; i++)
            {
                var index = sortRun.Start + i;
                var value = list[index].ToString();

                if (index == firstIndex)
                    value = "(" + value + ")";
                if (index == secondIndex)
                    value = "[" + value + "]";
                if (index == firstSource)
                    value = "<" + value + ">";

                strings[i] = value;
            }

            return string.Join(", ", strings.Select(x => x));
        }
    }
}
