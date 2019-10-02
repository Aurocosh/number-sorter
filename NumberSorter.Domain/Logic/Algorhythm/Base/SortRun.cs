namespace NumberSorter.Domain.Logic.Algorhythm
{
    public sealed class SortRun
    {
        public int Start { get; }
        public int Length { get; }

        public SortRun() : this(0, 0) { }

        public SortRun(int start, int length)
        {
            Start = start;
            Length = length;
        }

        public override string ToString()
        {
            return $"Start: {Start} Length: {Length}";
        }
    }
}