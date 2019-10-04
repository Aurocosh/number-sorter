namespace NumberSorter.Domain.Tests
{
    public struct Vector2Int
    {
        public int Min { get; }
        public int Max { get; }

        public Vector2Int(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}