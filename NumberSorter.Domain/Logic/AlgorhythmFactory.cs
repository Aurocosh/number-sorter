using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm.QuickSort.PivotSelectors;
using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class AlgorhythmFactory
    {
        public static ISortAlgorhythm<T> GetAlgorhythm<T>(AlgorhythmType algorhythmType, IComparer<T> comparer)
        {
            switch (algorhythmType)
            {
                case AlgorhythmType.TimSort:
                    return new TimSort<T>(comparer);
                case AlgorhythmType.HeapSort:
                    return new HeapSort<T>(comparer);
                case AlgorhythmType.BubbleSort:
                    return new BubbleSort<T>(comparer);
                case AlgorhythmType.InsertionSort:
                    return new InsertionSort<T>(comparer);
                case AlgorhythmType.DequeMergeSort:
                    return new DequeMergeSort<T>(comparer);
                case AlgorhythmType.InPlaceMergeSort:
                    return new InPlaceMergeSort<T>(comparer);
                case AlgorhythmType.RecursiveMergeSort:
                    return new RecursiveMergeSort<T>(comparer);
                case AlgorhythmType.QuickSortRandomPivot:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>());
                case AlgorhythmType.QuickSortMedianOfThree:
                    return new QuickSort<T>(comparer, new MedianThreePivotSelector<T>());
                default:
                    return null;
            }
        }
    }
}