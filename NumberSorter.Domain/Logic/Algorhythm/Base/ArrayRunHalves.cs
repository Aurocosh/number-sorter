namespace NumberSorter.Domain.Logic.Algorhythm
{
    public sealed class ArrayRunHalves
    {
        public SortRun First { get; }
        public SortRun Second { get; }

        public ArrayRunHalves(SortRun first, SortRun second)
        {
            First = first;
            Second = second;
        }
    }
}
