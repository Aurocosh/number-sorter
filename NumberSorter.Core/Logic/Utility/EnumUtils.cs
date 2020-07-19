using System;

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
