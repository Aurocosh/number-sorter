using NumberSorter.Core.Algorhythm;
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
                case AlgorhythmType.CombSort:
                    return new CombSort<T>(comparer);
                case AlgorhythmType.HeapSort:
                    return new HeapSort<T>(comparer);
                case AlgorhythmType.BinarySort:
                    return new BinarySort<T>(comparer);
                case AlgorhythmType.BubbleSort:
                    return new BubbleSort<T>(comparer);
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
                case AlgorhythmType.GallopingRecursiveMergeSort:
                    return new GallopingRecursiveMergeSort<T>(comparer);
                case AlgorhythmType.QuickSortRandomPivot:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>(), x => new DummySort<T>(x), 0);
                case AlgorhythmType.QuickSortMedianOfThree:
                    return new QuickSort<T>(comparer, new MedianThreePivotSelector<T>(), x => new DummySort<T>(x), 0);
                case AlgorhythmType.InsertionWindowTimSort:
                    return new TimSort<T>(comparer, x => new InsertionSort<T>(x), x => new WindowMergeSort<T>(x));
                case AlgorhythmType.WindowWindowTimSort:
                    return new TimSort<T>(comparer, x => new WindowMergeSort<T>(x), x => new WindowMergeSort<T>(x));
                case AlgorhythmType.QuickSortRandomPivotCutoffInsertion:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>(), x => new InsertionSort<T>(x), 32);
                case AlgorhythmType.InsertionTripleWindowTimSort:
                    return new TimSort<T>(comparer, x => new InsertionSort<T>(x), x => new TripleWindowMergeSort<T>(x));
                case AlgorhythmType.QuickSortRandomPivotCutoffWindow:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>(), x => new WindowMergeSort<T>(x), 32);
                case AlgorhythmType.QuickSortRandomPivotCutoffTripleWindow:
                    return new QuickSort<T>(comparer, new RandomPivotSelector<T>(), x => new TripleWindowMergeSort<T>(x), 32);
                case AlgorhythmType.TripleWindowTripleWindowTimSort:
                    return new TimSort<T>(comparer, x => new TripleWindowMergeSort<T>(x), x => new TripleWindowMergeSort<T>(x));
                default:
                    return null;
            }
        }
    }
}