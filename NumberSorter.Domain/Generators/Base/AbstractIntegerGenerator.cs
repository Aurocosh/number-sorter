﻿using System;

namespace NumberSorter.Domain.Generators
{
    public abstract class AbstractRandomGenerator
    {
        protected readonly Random Random;

        protected AbstractRandomGenerator(Random random)
        {
            Random = random;
        }
    }
}
