namespace NumberSorter.Domain.Logic
{
    public enum LocalMergeType
    {
        IntervalLinearSearch,
        IntervalBinarySearch,
        IntervalBiasedBinarySearch,
        IntervalCustomBiasedBinarySearch,
        Window,
        TripleWindow,
        Buffer,
        Deque,
        Gallop,
        HalfInPlace,
        KindaInPlace,
        Rotation,
        BufferRotation,
    }
}
