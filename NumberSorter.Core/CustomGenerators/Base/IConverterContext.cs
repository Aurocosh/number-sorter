using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.CustomGenerators.Base
{
    public interface IConverterContext
    {
        Random Random { get; }
    }
}
