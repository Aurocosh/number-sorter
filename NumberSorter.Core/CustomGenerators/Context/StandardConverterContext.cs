using NumberSorter.Core.CustomGenerators.Base;
using System;

namespace NumberSorter.Core.CustomGenerators.Context
{
    public class StandardConverterContext : IConverterContext
    {
        public Random Random { get; } = new Random();
    }
}
