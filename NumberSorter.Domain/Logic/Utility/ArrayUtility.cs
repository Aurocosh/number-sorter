using NumberSorter.Domain.Logic.Algorhythm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberSorter.Domain.Logic.Utility
{
    public static class ArrayUtility
    {
        public static ArrayHalves<T> SplitArray<T>(T[] array)
        {
            if (array.Length == 0)
                return new ArrayHalves<T>(new T[0], new T[0]);
            if (array.Length == 1)
                return new ArrayHalves<T>(array.ToArray(), new T[0]);

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
