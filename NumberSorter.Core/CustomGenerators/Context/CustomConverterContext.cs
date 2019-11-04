using NumberSorter.Core.CustomGenerators.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
