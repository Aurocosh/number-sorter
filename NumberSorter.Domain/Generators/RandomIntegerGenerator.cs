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

        public List<int> Generate(int minimum, int maximum, int count)
        {
            var numbers = new List<int>(count);
            for (int i = 0; i < count; i++)
                numbers.Add(_random.Next(minimum, maximum));
            return numbers;
        }
    }
}
