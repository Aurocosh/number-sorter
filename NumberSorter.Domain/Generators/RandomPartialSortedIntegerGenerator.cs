using System;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Domain.Generators
{
    public class RandomPartialSortedIntegerGenerator
    {
        private readonly Random _random;

        public RandomPartialSortedIntegerGenerator()
        {
            _random = new Random();
        }

        public List<int> Generate(int minimumValue, int maximumValue, int minRunSize, int maxRunSize, int minRunStep, int maxRunStep, int count, double randomRunProbability)
        {
            //Contract.Requires<ArgumentException>(minimumValue > maximumValue, $"Value of {nameof(maximumValue)} cannot be less then value of {nameof(minimumValue)}");
            //Contract.Requires<ArgumentException>(minRunSize > maxRunSize, $"Value of {nameof(maxRunSize)} cannot be less then value of {nameof(minRunSize)}");
            //Contract.Requires<ArgumentException>(minRunStep > maxRunStep, $"Value of {nameof(maxRunStep)} cannot be less then value of {nameof(minRunStep)}");
            //Contract.Requires<ArgumentException>(minRunSize < 1, $"Value of {nameof(minRunSize)} cannot be less then 1");
            //Contract.Requires<ArgumentException>(minRunStep < 0, $"Value of {nameof(minRunStep)} cannot be less then 0");
            //Contract.Requires<ArgumentException>(maxRunStep < 1, $"Value of {nameof(maxRunStep)} cannot be less then 1");
            //Contract.Requires<ArgumentException>(count < 0, $"Value of {nameof(count)} cannot be negative");

            var limiter = 50000;
            var numbers = new List<int>(count);
            while (count > 0 && limiter-- > 0)
            {
                maxRunSize = Math.Min(maxRunSize, count);
                minRunSize = Math.Min(minRunSize, count);
                int runSize = _random.Next(minRunSize, maxRunSize + 1);
                int value = _random.Next(minimumValue, maximumValue + 1);
                int sign = (_random.Next(0, 2) * 2) - 1;
                bool isRandom = _random.NextDouble() < randomRunProbability;

                for (int i = 0; i < runSize; i++)
                {
                    try
                    {
                        if (isRandom)
                            value = _random.Next(minimumValue, maximumValue + 1);
                        else
                            value = checked(value + (sign * _random.Next(minRunStep, maxRunStep + 1)));
                    }
                    catch (OverflowException)
                    {
                    }
                    numbers.Add(value);
                }
                count -= runSize;
            }
            return numbers;
        }
    }
}
