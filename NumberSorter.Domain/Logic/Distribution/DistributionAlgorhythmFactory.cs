using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.IntegerSort;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator;
using NumberSorter.Domain.DialogService;
using ReactiveUI;

namespace NumberSorter.Domain.Logic
{
    public static class DistributionAlgorhythmFactory
    {
        public static IIntegerSortAlgorhythm GetAlgorhythm(DistributionAlgorhythmType algorhythmType, IDialogService<ReactiveObject> dialogService)
        {
            switch (algorhythmType)
            {
                case DistributionAlgorhythmType.AmericanFlagSort:
                    return new AmericanFlagSort(10, new OptimizedLocalSignSeparator());
                case DistributionAlgorhythmType.AverageBucketSort:
                    return new AverageBucketSort();

                case DistributionAlgorhythmType.BitLSDRadixSort:
                    return new BitLSDRadixSort();
                case DistributionAlgorhythmType.BitLSDOptimizedRadixSort:
                    return new BitLSDOptimizedRadixSort(new OptimizedLocalSignSeparator());

                case DistributionAlgorhythmType.BitMSDRadixSort:
                    return new BitMSDRadixSort();
                case DistributionAlgorhythmType.BitMSDOptimizedRadixSort:
                    return new BitMSDOptimizedRadixSort(new OptimizedLocalSignSeparator());

                case DistributionAlgorhythmType.LSDRadixSortBase2:
                    return new LSDRadixSort(2, new OptimizedLocalSignSeparator());
                case DistributionAlgorhythmType.LSDRadixSortBase4:
                    return new LSDRadixSort(4, new OptimizedLocalSignSeparator());
                case DistributionAlgorhythmType.LSDRadixSortBase16:
                    return new LSDRadixSort(16, new OptimizedLocalSignSeparator());

                case DistributionAlgorhythmType.MSDRadixSortBase2:
                    return new MSDRadixSort(2, new OptimizedLocalSignSeparator());
                case DistributionAlgorhythmType.MSDRadixSortBase4:
                    return new MSDRadixSort(4, new OptimizedLocalSignSeparator());
                case DistributionAlgorhythmType.MSDRadixSortBase16:
                    return new MSDRadixSort(16, new OptimizedLocalSignSeparator());

                default:
                    return null;
            }
        }
    }
}