using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Utility
{
    public static class EnumUtil
    {
        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
