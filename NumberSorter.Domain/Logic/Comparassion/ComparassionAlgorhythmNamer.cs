using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class ComparassionAlgorhythmNamer
    {
        private static readonly Dictionary<ComparassionAlgorhythmType, string> _nameDictionary = new Dictionary<ComparassionAlgorhythmType, string>();

        static ComparassionAlgorhythmNamer()
        {
            _nameDictionary.Add(ComparassionAlgorhythmType.CombSort, "Comb sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.CycleSort, "Cycle sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.GnomeSort, "Gnome sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.BubbleSort, "Bubble sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.CircleSort, "Circle sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.SmoothSort, "Smooth sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.PancakeSort, "Pancake sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.OddEvenSort, "Odd even sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.CocktailShakerSort, "Cocktail shaker sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.TimSortWindowWindow, "Tim Sort (Window sort, Window merge)");
            _nameDictionary.Add(ComparassionAlgorhythmType.TimSortBinaryInterval, "Tim Sort (Binary sort, Interval merge)");
            _nameDictionary.Add(ComparassionAlgorhythmType.TimSortInsertionWindow, "Tim Sort (Insertion sort, Window merge)");
            _nameDictionary.Add(ComparassionAlgorhythmType.TimSortInsertionTripleWindow, "Tim Sort (Insertion sort, Triple window merge)");
            _nameDictionary.Add(ComparassionAlgorhythmType.TimSortTripleWindowTripleWindow, "Tim Sort (Triple window sort, Triple window merge)");

            _nameDictionary.Add(ComparassionAlgorhythmType.DequeMergeSort, "Deque merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.WindowMergeSort, "Window merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMergeSort, "Interval merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.RecursiveMergeSort, "Recursive merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.HalfInPlaceMergeSort, "Half in place merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.TripleWindowMergeSort, "Triple window merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.KindaInPlaceMergeSort, "Kinda in place merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.WorkAreaInPlaceMergeSort, "Work area in place merge sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.InsertionSort, "Insertion sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.BinarySort, "Binary sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.SelectionSort, "Selection sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.DoubleSelectionSort, "Double selection sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.HeapSort, "Heap sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.JHeapBinarySort, "J heap sort (Binary sort)");
            _nameDictionary.Add(ComparassionAlgorhythmType.JHeapInsertionSort, "J heap sort (Insertion sort)");

            _nameDictionary.Add(ComparassionAlgorhythmType.ShellSortCiura, "Shell sort (Ciura)");
            _nameDictionary.Add(ComparassionAlgorhythmType.ShellSortKnuth, "Shell sort (Knuth)");
            _nameDictionary.Add(ComparassionAlgorhythmType.ShellSortTokuda, "Shell sort (Tokuda)");

            _nameDictionary.Add(ComparassionAlgorhythmType.MultiMergeGroupLinearSort, "Multi merge sort (Grouped runs, Linear search)");
            _nameDictionary.Add(ComparassionAlgorhythmType.MultiMergeSimpleLinearSort, "Multi merge sort (Simple runs, Linear search)");
            _nameDictionary.Add(ComparassionAlgorhythmType.MultiMergeGroupBinarySort, "Multi merge sort (Grouped runs, Binary search)");
            _nameDictionary.Add(ComparassionAlgorhythmType.MultiMergeSimpleBinarySort, "Multi merge sort (Simple runs, Binary search)");
            _nameDictionary.Add(ComparassionAlgorhythmType.MultiMergeGroupBinaryWindowSort, "Multi merge sort (Grouped runs, Binary search, Window merge sort runs)");
            _nameDictionary.Add(ComparassionAlgorhythmType.MultiMergeGroupBinaryRecursiveSort, "Multi merge sort (Simple runs, Binary search, Recursive)");

            _nameDictionary.Add(ComparassionAlgorhythmType.KWayMergeSortGroup, "K way merge sort (Grouped runs)");
            _nameDictionary.Add(ComparassionAlgorhythmType.KWayMergeSortSimple, "K way merge sort (Simple runs)");

            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMultiMergeGroupLinearSort, "Interval multi merge sort (Simple runs, Linear search)");
            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMultiMergeGroupBinarySort, "Interval multi merge sort (Grouped runs, Binary search)");
            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMultiMergeGroupBiasedBinarySort, "Interval multi merge sort (Grouped runs, Biased binary search)");

            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortRandomPivot, "Quick sort LR (Random pivot)");
            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortMedianPivot, "Quick sort LR (Median of three pivot)");
            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortRandomPivotCutoffInsertion, "Quick sort LR (Median of three pivot, Insertion cutoff)");
            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortRandomPivotCutoffTripleWindow, "Quick sort LR (Median of three pivot, Triple window cutoff)");

            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortLLRandomPivot, "Quick sort LL (Random pivot)");
            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortLLMedianPivot, "Quick sort LL (Median of three pivot)");

            _nameDictionary.Add(ComparassionAlgorhythmType.DualPivotQuickSort, "Dual pivot quick sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.DualPivotQuickSortCutoffInsertion, "Dual pivot quick sort (Insertion cutoff)");

            _nameDictionary.Add(ComparassionAlgorhythmType.StableQuickSortRandomPivot, "Stable quick sort (Random pivot)");
            _nameDictionary.Add(ComparassionAlgorhythmType.StableQuickSortMedianPivot, "Stable quick sort (Median of three pivot)");
        }

        public static string GetName(ComparassionAlgorhythmType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}