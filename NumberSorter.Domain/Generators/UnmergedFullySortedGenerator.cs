using NumberSorter.Domain.Logic.Container;
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

            for (int i = 0; i < size; i++)
                numbers[i] = _random.Next(minimumValue, maximumValue + 1);
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
