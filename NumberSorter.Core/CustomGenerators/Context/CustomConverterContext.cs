using NumberSorter.Core.CustomGenerators.Base;
using System;

namespace NumberSorter.Core.CustomGenerators.Context
{
    public class CustomConverterContext : IConverterContext
    {
        public Random Random { get; }

        public CustomConverterContext(Random random)
        {
            Random = random;
        }
    }
}
