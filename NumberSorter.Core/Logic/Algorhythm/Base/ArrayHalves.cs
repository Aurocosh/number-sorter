namespace NumberSorter.Core.Logic.Algorhythm
{
    public sealed class ArrayHalves<T>
    {
        public T[] First { get; }
        public T[] Second { get; }

        public ArrayHalves(T[] first, T[] second)
        {
            First = first;
            Second = second;
        }
    }
}
