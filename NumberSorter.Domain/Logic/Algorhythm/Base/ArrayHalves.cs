namespace NumberSorter.Domain.Logic.Algorhythm
{
    public sealed class ArrayHalves<K>
    {
        public K[] First { get; }
        public K[] Second { get; }

        public ArrayHalves(K[] first, K[] second)
        {
            First = first;
            Second = second;
        }
    }
}
