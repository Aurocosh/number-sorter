using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.GapGenerator;
using NumberSorter.Core.Logic.Algorhythm.QuickSort.PivotSelectors;
using NumberSorter.Core.Logic.Algorhythm.SortRunLocator;
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
                case AlgorhythmType.HeapSort:
                    return new HeapSort<T>(comparer);
                case AlgorhythmType.CycleSort:
                    return new CycleSort<T>(comparer);
                case AlgorhythmType.GnomeSort:
                    return new GnomeSort<T>(comparer);
                case AlgorhythmType.BinarySort:
                    return new BinarySort<T>(comparer);
                case AlgorhythmType.CircleSort:
                    return new CircleSort<T>(comparer);
                case AlgorhythmType.BubbleSort:
                    return new BubbleSort<T>(comparer);
                case AlgorhythmType.PancakeSort:
                    return new PancakeSort<T>(comparer);
                case AlgorhythmType.OddEvenSort:
                    return new OddEvenSort<T>(comparer);
                case AlgorhythmType.InsertionSort:
                    return new InsertionSort<T>(comparer);
                case AlgorhythmType.SelectionSort:
                    return new SelectionSort<T>(comparer);
                case AlgorhythmType.DequeMergeSort:
                    return new DequeMergeSort<T>(comparer);
                case AlgorhythmType.WindowMergeSort:
                    return new WindowMergeSort<T>(comparer);
                case AlgorhythmType.CocktailShakerSort:
                    return new CocktailShakerSort<T>(comparer);
                case AlgorhythmType.RecursiveMergeSort:
                    return new RecursiveMergeSort<T>(comparer);
                case AlgorhythmType.DoubleSelectionSort:
                    return new DoubleSelectionSort<T>(comparer);
                case AlgorhythmType.HalfInPlaceMergeSort:
                    return new HalfInPlaceMergeSort<T>(comparer);
                case AlgorhythmType.KindaInPlaceMergeSort:
                    return new KindaInPlaceMergeSort<T>(comparer);
                case AlgorhythmType.TripleWindowMergeSort:
                    return new TripleWindowMergeSort<T>(comparer);
                case AlgorhythmType.WorkAreaInPlaceMergeSort:
                    return new WorkAreaInPlaceMergeSort<T>(comparer);
                case AlgorhythmType.GallopingRecursiveMergeSort:
                    return new GallopingRecursiveMergeSort<T>(comparer);
                case AlgorhythmType.ShellSortCiura:
                    return new ShellSort<T>(comparer, new CiuraGapGenerator());
                case AlgorhythmType.ShellSortKnuth:
                    return new ShellSort<T>(comparer, new KnuthGapGenerator());
                case AlgorhythmType.ShellSortTokuda:
                    return new ShellSort<T>(comparer, new TokudaGapGenerator());
                case AlgorhythmType.InsertionWindowTimSort:
                    return new TimSort<T>(comparer, x => new InsertionSort<T>(x), x => new WindowMergeSort<T>(x));
                case AlgorhythmType.WindowWindowTimSort:
                    return new TimSort<T>(comparer, x => new WindowMergeSort<T>(x), x => new WindowMergeSort<T>(x));
                case AlgorhythmType.InsertionTripleWindowTimSort:
                    return new TimSort<T>(comparer, x => new InsertionSort<T>(x), x => new TripleWindowMergeSort<T>(x));
                case AlgorhythmType.TripleWindowTripleWindowTimSort:
                    return new TimSort<T>(comparer, x => new TripleWindowMergeSort<T>(x), x => new TripleWindowMergeSort<T>(x));

                case AlgorhythmType.RecursiveGroupMultiMergeSort:
                    return new RecursiveMultiMergeSort<T>(comparer, new GroupingRunLocator<T>(comparer, new InsertionSort<T>(comparer), 32), x => new GroupingRunLocator<SortRun>(x, new InsertionSort<SortRun>(x), 32));
                case AlgorhythmType.RecursiveSimpleMultiMergeSort:
                    return new RecursiveMultiMergeSort<T>(comparer, new SimpleRunLocator<T>(comparer), x => new SimpleRunLocator<SortRun>(x));
                case AlgorhythmType.SimpleMultiMergeSort:
                    return new MultiMergeSort<T>(comparer, new SimpleRunLocator<T>(comparer), x => new InsertionSort<SortRun>(x));
                case AlgorhythmType.GroupMultiMergeSort:
                    return new MultiMergeSort<T>(comparer, new GroupingRunLocator<T>(comparer, new InsertionSort<T>(comparer), 32), x => new InsertionSort<SortRun>(x));

                case AlgorhythmType.QuickSortRandomPivot:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>(), x => new DummySort<T>(x), 0);
                case AlgorhythmType.QuickSortMedianOfThree:
                    return new QuickSort<T>(comparer, new MedianThreePivotSelector<T>(), x => new DummySort<T>(x), 0);
                case AlgorhythmType.QuickSortRandomPivotCutoffInsertion:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>(), x => new InsertionSort<T>(x), 32);
                case AlgorhythmType.QuickSortRandomPivotCutoffWindow:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>(), x => new WindowMergeSort<T>(x), 32);
                case AlgorhythmType.QuickSortRandomPivotCutoffTripleWindow:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>(), x => new TripleWindowMergeSort<T>(x), 32);

                default:
                    return null;
            }
        }
    }
}