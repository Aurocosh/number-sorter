using NumberSorter.Domain.Logic.Container;
using NumberSorter.Domain.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Domain.Generators
{
    public class UnmergedFullySortedGenerator
    {
        private readonly Random _random;

        public UnmergedFullySortedGenerator()
        {
            _random = new Random();
        }

        public List<int> Generate(int minimumValue, int maximumValue, int firstSize, int secondSize)
        {
            var size = secondSize + firstSize;
            var numbers = new int[size];

            IListUtility.Randomize(numbers, minimumValue, maximumValue);
            Array.Sort(numbers);

            var firstPart = new int[secondSize];
            var secondPart = new int[firstSize];

            Array.Copy(numbers, 0, firstPart, 0, secondSize);
            Array.Copy(numbers, secondSize, secondPart, 0, firstSize);

            Array.Copy(firstPart, 0, numbers, firstSize, secondSize);
            Array.Copy(secondPart, 0, numbers, 0, firstSize);
            return new List<int>(numbers);
        }
    }
}
