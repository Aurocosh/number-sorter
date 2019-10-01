using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Domain.Generators
{
    public class RandomIntegerGenerator
    {
        private readonly Random _random;

        public RandomIntegerGenerator()
        {
            _random = new Random();
        }

        public List<int> Generate(int minimumValue, int maximumValue, int count)
        {
            if (minimumValue > maximumValue)
                throw new ArgumentException($"Value of {nameof(maximumValue)} cannot be less then value of {nameof(minimumValue)}");

            var numbers = new List<int>(count);
            for (int i = 0; i < count; i++)
                numbers.Add(_random.Next(minimumValue, maximumValue + 1));
            return numbers;
        }
    }
}
