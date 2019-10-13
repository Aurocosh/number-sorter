using NumberSorter.Core.Logic.Algorhythm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Utility
{
    public static class ArrayUtility
    {
        public static T[] JoinArrays<T>(IReadOnlyList<T[]> arrays)
        {
            int finalSize = arrays.Sum(x => x.Length);
            var finalArray = new T[finalSize];

            int finalIndex = 0;
            foreach (var array in arrays)
            {
                Array.Copy(array, 0, finalArray, finalIndex, array.Length);
                finalIndex += array.Length;
            }

            return finalArray;
        }

        public static ArrayHalves<T> SplitArray<T>(T[] array)
        {
            if (array.Length == 0)
                return new ArrayHalves<T>(Array.Empty<T>(), Array.Empty<T>());
            if (array.Length == 1)
                return new ArrayHalves<T>(array.ToArray(), Array.Empty<T>());

            const int firstIndex = 0;
            int secondIndex = array.Length / 2;

            int firstLength = secondIndex;
            int secondLength = array.Length - firstLength;

            var firstArray = new T[firstLength];
            var secondArray = new T[secondLength];

            Array.Copy(array, firstIndex, firstArray, 0, firstLength);
            Array.Copy(array, secondIndex, secondArray, 0, secondLength);

            return new ArrayHalves<T>(firstArray, secondArray);
        }
    }
}
