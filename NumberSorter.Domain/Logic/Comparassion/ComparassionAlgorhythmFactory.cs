using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.GapGenerator;
using NumberSorter.Core.Logic.Factories.PivotSelector;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Domain.DialogService;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class ComparassionAlgorhythmFactory
    {
        public static ISortAlgorhythm<T> GetAlgorhythm<T>(ComparassionAlgorhythmType algorhythmType, IComparer<T> comparer, IDialogService<ReactiveObject> dialogService)
        {
            switch (algorhythmType)
            {
                case ComparassionAlgorhythmType.CombSort:
                    return new CombSort<T>(comparer);
                case ComparassionAlgorhythmType.CycleSort:
                    return new CycleSort<T>(comparer);
                case ComparassionAlgorhythmType.GnomeSort:
                    return new GnomeSort<T>(comparer);
                case ComparassionAlgorhythmType.CircleSort:
                    return new CircleSort<T>(comparer);
                case ComparassionAlgorhythmType.SmoothSort:
                    return new SmoothSort<T>(comparer);
                case ComparassionAlgorhythmType.BubbleSort:
                    return new BubbleSort<T>(comparer);
                case ComparassionAlgorhythmType.PancakeSort:
                    return new PancakeSort<T>(comparer);
                case ComparassionAlgorhythmType.OddEvenSort:
                    return new OddEvenSort<T>(comparer);
                case ComparassionAlgorhythmType.CocktailShakerSort:
                    return new CocktailShakerSort<T>(comparer);

                case ComparassionAlgorhythmType.DequeMergeSort:
                    return new DequeMergeSort<T>(comparer);
                case ComparassionAlgorhythmType.WindowMergeSort:
                    return new WindowMergeSort<T>(comparer);
                case ComparassionAlgorhythmType.RecursiveMergeSort:
                    return new RecursiveMergeSort<T>(comparer);
                case ComparassionAlgorhythmType.HalfInPlaceMergeSort:
                    return new HalfInPlaceMergeSort<T>(comparer);
                case ComparassionAlgorhythmType.KindaInPlaceMergeSort:
                    return new KindaInPlaceMergeSort<T>(comparer);
                case ComparassionAlgorhythmType.WorkAreaInPlaceMergeSort:
                    return new WorkAreaInPlaceMergeSort<T>(comparer);
                case ComparassionAlgorhythmType.IntervalMergeSort:
                    return new IntervalMergeSort<T>(comparer, new BiasedBinaryPositionLocatorFactory(2));
                case ComparassionAlgorhythmType.TripleWindowMergeSort:
                    return new TripleWindowMergeSort<T>(comparer);

                case ComparassionAlgorhythmType.BinarySort:
                    return new BinarySort<T>(comparer);
                case ComparassionAlgorhythmType.InsertionSort:
                    return new InsertionSort<T>(comparer);

                case ComparassionAlgorhythmType.SelectionSort:
                    return new SelectionSort<T>(comparer);
                case ComparassionAlgorhythmType.DoubleSelectionSort:
                    return new DoubleSelectionSort<T>(comparer);

                case ComparassionAlgorhythmType.HeapSort:
                    return new HeapSort<T>(comparer);
                case ComparassionAlgorhythmType.JHeapBinarySort:
                    return new JHeapSort<T>(comparer, new BinarySortFactory());
                case ComparassionAlgorhythmType.JHeapInsertionSort:
                    return new JHeapSort<T>(comparer, new InsertionSortFactory());

                case ComparassionAlgorhythmType.TimSortBinaryInterval:
                    return new TimSort<T>(comparer, new IntervalMergeSortFactory(new BiasedBinaryPositionLocatorFactory(2)), new BinarySortFactory());
                case ComparassionAlgorhythmType.TimSortInsertionWindow:
                    return new TimSort<T>(comparer, new WindowMergeSortFactory(), new InsertionSortFactory());
                case ComparassionAlgorhythmType.TimSortWindowWindow:
                    return new TimSort<T>(comparer, new WindowMergeSortFactory(), new WindowMergeSortFactory());
                case ComparassionAlgorhythmType.TimSortInsertionTripleWindow:
                    return new TimSort<T>(comparer, new TripleWindowMergeSortFactory(), new InsertionSortFactory());
                case ComparassionAlgorhythmType.TimSortTripleWindowTripleWindow:
                    return new TimSort<T>(comparer, new TripleWindowMergeSortFactory(), new TripleWindowMergeSortFactory());

                case ComparassionAlgorhythmType.ShellSortCiura:
                    return new ShellSort<T>(comparer, new CiuraGapGenerator());
                case ComparassionAlgorhythmType.ShellSortKnuth:
                    return new ShellSort<T>(comparer, new KnuthGapGenerator());
                case ComparassionAlgorhythmType.ShellSortTokuda:
                    return new ShellSort<T>(comparer, new TokudaGapGenerator());

                case ComparassionAlgorhythmType.MultiMergeSimpleLinearSort:
                    return new MultiMergeSort<T>(comparer, new InsertionSortFactory(), new SimpleRunLocatorFactory(), new LinearPositionLocatorFactory());
                case ComparassionAlgorhythmType.MultiMergeGroupLinearSort:
                    return new MultiMergeSort<T>(comparer, new InsertionSortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new LinearPositionLocatorFactory());
                case ComparassionAlgorhythmType.MultiMergeSimpleBinarySort:
                    return new MultiMergeSort<T>(comparer, new BinarySortFactory(), new SimpleRunLocatorFactory(), new BinaryPositionLocatorFactory());
                case ComparassionAlgorhythmType.MultiMergeGroupBinarySort:
                    return new MultiMergeSort<T>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BinaryPositionLocatorFactory());
                case ComparassionAlgorhythmType.MultiMergeGroupBinaryWindowSort:
                    return new MultiMergeSort<T>(comparer, new WindowMergeSortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BinaryPositionLocatorFactory());
                case ComparassionAlgorhythmType.MultiMergeGroupBinaryRecursiveSort:
                    return new RecursiveMultiMergeSortFactory(new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BinaryPositionLocatorFactory()).GetSort(comparer);

                case ComparassionAlgorhythmType.KWayMergeSortSimple:
                    return new KWayMergeSort<T>(comparer, new BinarySortFactory(), new SimpleRunLocatorFactory(), new BiasedBinaryPositionLocatorFactory(2), 8);
                case ComparassionAlgorhythmType.KWayMergeSortGroup:
                    return new KWayMergeSort<T>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BiasedBinaryPositionLocatorFactory(2), 8);

                case ComparassionAlgorhythmType.IntervalMultiMergeGroupLinearSort:
                    return new IntervalMultiMergeSort<T>(comparer, new InsertionSortFactory(), new SimpleRunLocatorFactory(), new LinearPositionLocatorFactory());
                case ComparassionAlgorhythmType.IntervalMultiMergeGroupBinarySort:
                    return new IntervalMultiMergeSort<T>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BinaryPositionLocatorFactory());
                case ComparassionAlgorhythmType.IntervalMultiMergeGroupBiasedBinarySort:
                    return new IntervalMultiMergeSort<T>(comparer, new BinarySortFactory(), new GroupingRunLocatorFactory(32, new BinarySortFactory()), new BiasedBinaryPositionLocatorFactory(2));

                case ComparassionAlgorhythmType.QuickSortRandomPivot:
                    return new QuickSort<T>(comparer, new RandomPivotSelectorFactory(new Random()), new InsertionSortFactory(), 8);
                case ComparassionAlgorhythmType.QuickSortMedianPivot:
                    return new QuickSort<T>(comparer, new MedianOfThreePivotSelectorFactory(), new InsertionSortFactory(), 8);
                case ComparassionAlgorhythmType.QuickSortRandomPivotCutoffWindow:
                    return new QuickSort<T>(comparer, new RandomPivotSelectorFactory(new Random()), new WindowMergeSortFactory(), 32);
                case ComparassionAlgorhythmType.QuickSortRandomPivotCutoffTripleWindow:
                    return new QuickSort<T>(comparer, new RandomPivotSelectorFactory(new Random()), new TripleWindowMergeSortFactory(), 32);

                case ComparassionAlgorhythmType.QuickSortLLRandomPivot:
                    return new QuickSortLL<T>(comparer, new RandomPivotSelectorFactory(new Random()), new InsertionSortFactory(), 8);
                case ComparassionAlgorhythmType.QuickSortLLMedianPivot:
                    return new QuickSortLL<T>(comparer, new MedianOfThreePivotSelectorFactory(), new InsertionSortFactory(), 8);

                case ComparassionAlgorhythmType.DualPivotQuickSort:
                    return new DualPivotQuickSort<T>(comparer, new InsertionSortFactory(), 0);
                case ComparassionAlgorhythmType.DualPivotQuickSortCutoffInsertion:
                    return new DualPivotQuickSort<T>(comparer, new InsertionSortFactory(), 16);

                case ComparassionAlgorhythmType.StableQuickSortRandomPivot:
                    return new StableQuickSort<T>(comparer, new RandomPivotSelectorFactory(new Random()), new InsertionSortFactory(), 8);
                case ComparassionAlgorhythmType.StableQuickSortMedianPivot:
                    return new StableQuickSort<T>(comparer, new MedianOfThreePivotSelectorFactory(), new InsertionSortFactory(), 8);

                default:
                    return null;
            }
        }
    }
}