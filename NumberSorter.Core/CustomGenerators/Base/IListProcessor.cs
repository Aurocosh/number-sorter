using NumberSorter.Core.CustomGenerators.Base;
using System;

namespace NumberSorter.Core.CustomGenerators
{
    public interface IListProcessor : ICloneable
    {
        string Description { get; }
        void ConvertList(ref int[] list, IConverterContext context);
    }
}
