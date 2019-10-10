﻿using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.QuickSort.PivotSelectors;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic
{
    public static class AlgorhythmFactory
    {
        public static ISortAlgorhythm<T> GetAlgorhythm<T>(AlgorhythmType algorhythmType, IComparer<T> comparer)
        {
            switch (algorhythmType)
            {
                case AlgorhythmType.HeapSort:
                    return new HeapSort<T>(comparer);
                case AlgorhythmType.BinarySort:
                    return new BinarySort<T>(comparer);
                case AlgorhythmType.BubbleSort:
                    return new BubbleSort<T>(comparer);
                case AlgorhythmType.InsertionSort:
                    return new InsertionSort<T>(comparer);
                case AlgorhythmType.DequeMergeSort:
                    return new DequeMergeSort<T>(comparer);
                case AlgorhythmType.WindowMergeSort:
                    return new WindowMergeSort<T>(comparer);
                case AlgorhythmType.RecursiveMergeSort:
                    return new RecursiveMergeSort<T>(comparer);
                case AlgorhythmType.HalfInPlaceMergeSort:
                    return new HalfInPlaceMergeSort<T>(comparer);
                case AlgorhythmType.KindaInPlaceMergeSort:
                    return new KindaInPlaceMergeSort<T>(comparer);
                case AlgorhythmType.GallopingRecursiveMergeSort:
                    return new GallopingRecursiveMergeSort<T>(comparer);
                case AlgorhythmType.QuickSortRandomPivot:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>());
                case AlgorhythmType.QuickSortMedianOfThree:
                    return new QuickSort<T>(comparer, new MedianThreePivotSelector<T>());
                case AlgorhythmType.InsertionWindowTimSort:
                    return new TimSort<T>(comparer, x => new InsertionSort<T>(x), x => new WindowMergeSort<T>(x));
                default:
                    return null;
            }
        }
    }
}