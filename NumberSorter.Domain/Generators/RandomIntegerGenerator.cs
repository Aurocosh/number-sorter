﻿using NumberSorter.Domain.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberSorter.Domain.Generators
{
    public class RandomIntegerGenerator : AbstractRandomGenerator
    {
        public RandomIntegerGenerator(Random random) : base(random)
        {
        }

        public List<int> Generate(int minimumValue, int maximumValue, int count)
        {
            if (minimumValue > maximumValue)
                throw new ArgumentException($"Value of {nameof(maximumValue)} cannot be less then value of {nameof(minimumValue)}");

            var numbers = new int[count];
            IListUtility.Randomize(numbers, minimumValue, maximumValue, Random);
            return new List<int>(numbers);
        }
    }
}
