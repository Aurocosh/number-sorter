using NumberSorter.Core.CustomGenerators.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.CustomGenerators
{
    public interface IListProcessor : ICloneable
    {
        string Description { get; }
        void ConvertList(ref int[] list, IConverterContext context);
    }
}
