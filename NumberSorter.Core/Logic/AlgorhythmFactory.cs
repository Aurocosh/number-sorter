﻿using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.GapGenerator;
using NumberSorter.Core.Logic.Factories.PivotSelector;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic
{
    public static class AlgorhythmFactory
    {
        public static ISortAlgorhythm<T> GetAlgorhythm<T>(AlgorhythmType algorhythmType, IComparer<T> comparer)
        {
            switch (algorhythmType)
            {
                case AlgorhythmType.CombSort:
                    return new CombSort<T>(comparer);
                case AlgorhythmType.CycleSort:
                    return new CycleSort<T>(comparer);
                case AlgorhythmType.GnomeSort:
                    return new GnomeSort<T>(comparer);
                case AlgorhythmType.CircleSort:
                    return new CircleSort<T>(comparer);
                case AlgorhythmType.BubbleSort:
                    return new BubbleSort<T>(comparer);
                case AlgorhythmType.PancakeSort:
                    return new PancakeSort<T>(comparer);
                case AlgorhythmType.OddEvenSort:
                    return new OddEvenSort<T>(comparer);
                case AlgorhythmType.DequeMergeSort:
                    return new DequeMergeSort<T>(comparer);
                case AlgorhythmType.WindowMergeSort:
                    return new WindowMergeSort<T>(comparer);
                case AlgorhythmType.CocktailShakerSort:
                    return new CocktailShakerSort<T>(comparer);
                case AlgorhythmType.RecursiveMergeSort:
                    return new RecursiveMergeSort<T>(comparer);
                case AlgorhythmType.HalfInPlaceMergeSort:
                    return new HalfInPlaceMergeSort<T>(comparer);
                case AlgorhythmType.KindaInPlaceMergeSort:
                    return new KindaInPlaceMergeSort<T>(comparer);
                case AlgorhythmType.TripleWindowMergeSort:
                    return new TripleWindowMergeSort<T>(comparer);
                case AlgorhythmType.WorkAreaInPlaceMergeSort:
                    return new WorkAreaInPlaceMergeSort<T>(comparer);

                case AlgorhythmType.BinarySort:
                    return new BinarySort<T>(comparer);
                case AlgorhythmType.InsertionSort:
                    return new InsertionSort<T>(comparer);

                case AlgorhythmType.SelectionSort:
                    return new SelectionSort<T>(comparer);
                case AlgorhythmType.DoubleSelectionSort:
                    return new DoubleSelectionSort<T>(comparer);

                case AlgorhythmType.HeapSort:
                    return new HeapSort<T>(comparer);
                case AlgorhythmType.JHeapBinarySort:
                    return new JHeapSort<T>(comparer, new BinarySortFactory());
                case AlgorhythmType.JHeapInsertionSort:
                    return new JHeapSort<T>(comparer, new InsertionSortFactory());

                case AlgorhythmType.InsertionWindowTimSort:
                    return new TimSort<T>(comparer, new WindowMergeSortFactory(), new InsertionSortFactory());
                case AlgorhythmType.WindowWindowTimSort:
                    return new TimSort<T>(comparer, new WindowMergeSortFactory(), new WindowMergeSortFactory());
                case AlgorhythmType.InsertionTripleWindowTimSort:
                    return new TimSort<T>(comparer, new TripleWindowMergeSortFactory(), new InsertionSortFactory());
                case AlgorhythmType.TripleWindowTripleWindowTimSort:
                    return new TimSort<T>(comparer, new TripleWindowMergeSortFactory(), new TripleWindowMergeSortFactory());

                case AlgorhythmType.ShellSortCiura:
                    return new ShellSort<T>(comparer, new CiuraGapGenerator());
                case AlgorhythmType.ShellSortKnuth:
                    return new ShellSort<T>(comparer, new KnuthGapGenerator());
                case AlgorhythmType.ShellSortTokuda:
                    return new ShellSort<T>(comparer, new TokudaGapGenerator());

                case AlgorhythmType.MultiMergeSimpleLinearSort:
                    return new MultiMergeSort<T>(comparer, new InsertionSortFactory(), new SimpleRunLocatorFactory(), new LinearPositionLocatorFactory());
                case AlgorhythmType.MultiMergeGroupLinearSort:
                    return new MultiMergeSort<T>(comparer, new InsertionSortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new LinearPositionLocatorFactory());
                case AlgorhythmType.MultiMergeSimpleBinarySort:
                    return new MultiMergeSort<T>(comparer, new BinarySortFactory(), new SimpleRunLocatorFactory(), new BinaryPositionLocatorFactory());
                case AlgorhythmType.MultiMergeGroupBinarySort:
                    return new MultiMergeSort<T>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BinaryPositionLocatorFactory());
                case AlgorhythmType.MultiMergeGroupBinaryWindowSort:
                    return new MultiMergeSort<T>(comparer, new WindowMergeSortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BinaryPositionLocatorFactory());
                case AlgorhythmType.MultiMergeGroupBinaryRecursiveSort:
                    return new RecursiveMultiMergeSortFactory(new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BinaryPositionLocatorFactory()).GetSort(comparer);

                case AlgorhythmType.IntervalMultiMergeGroupLinearSort:
                    return new IntervalMultiMergeSort<T>(comparer, new InsertionSortFactory(), new SimpleRunLocatorFactory(), new LinearPositionLocatorFactory());
                case AlgorhythmType.IntervalMultiMergeGroupBinarySort:
                    return new IntervalMultiMergeSort<T>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BinaryPositionLocatorFactory());
                case AlgorhythmType.IntervalMultiMergeGroupBiasedBinarySort:
                    return new IntervalMultiMergeSort<T>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BiasedBinaryPositionLocatorFactory(2));

                case AlgorhythmType.QuickSortRandomPivot:
                    return new QuickSort<T>(comparer, new RandomPivotSelectorFactory(new Random()), new InsertionSortFactory(), 0);
                case AlgorhythmType.QuickSortMedianOfThree:
                    return new QuickSort<T>(comparer, new MedianOfThreePivotSelectorFactory(), new InsertionSortFactory(), 0);
                case AlgorhythmType.QuickSortRandomPivotCutoffInsertion:
                    return new QuickSort<T>(comparer, new RandomPivotSelectorFactory(new Random()), new InsertionSortFactory(), 32);
                case AlgorhythmType.QuickSortRandomPivotCutoffWindow:
                    return new QuickSort<T>(comparer, new RandomPivotSelectorFactory(new Random()), new WindowMergeSortFactory(), 32);
                case AlgorhythmType.QuickSortRandomPivotCutoffTripleWindow:
                    return new QuickSort<T>(comparer, new RandomPivotSelectorFactory(new Random()), new TripleWindowMergeSortFactory(), 32);

                default:
                    return null;
            }
        }
    }
}