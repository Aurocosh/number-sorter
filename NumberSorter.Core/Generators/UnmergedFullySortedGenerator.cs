using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Generators
{
    public class UnmergedFullySortedGenerator : AbstractRandomGenerator
    {
        public UnmergedFullySortedGenerator(Random random) : base(random)
        {
        }

        public List<int> Generate(int minimumValue, int maximumValue, int firstSize, int secondSize)
        {
            var size = secondSize + firstSize;
            var numbers = new int[size];

            IListUtility.Randomize(numbers, minimumValue, maximumValue, Random);
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
