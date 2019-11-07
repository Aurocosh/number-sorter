using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;

namespace NumberSorter.Core.Logic
{
    public static class IntAlgorhythmFactory
    {
        public static IIntegerSortAlgorhythm GetAlgorhythm(AlgorhythmType algorhythmType)
        {
            switch (algorhythmType)
            {
                case AlgorhythmType.AverageBucketSort:
                    return new AverageBucketSort();

                default:
                    return null;
            }
        }
    }
}