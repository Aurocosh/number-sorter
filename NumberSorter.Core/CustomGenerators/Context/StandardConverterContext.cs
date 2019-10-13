using NumberSorter.Core.CustomGenerators.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.CustomGenerators.Context
{
    public class StandardConverterContext : IConverterContext
    {
        public Random Random { get; } = new Random();
    }
}
