namespace NumberSorter.Core.Logic.Utility
{
    public static class IntUtility
    {
        public static int IntPow(int value, uint power)
        {
            int result = 1;
            while (power != 0)
            {
                if ((power & 1) == 1)
                    result *= value;
                value *= value;
                power >>= 1;
            }
            return result;
        }
    }
}
