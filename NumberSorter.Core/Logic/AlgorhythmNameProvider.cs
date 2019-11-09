using System.Collections.Generic;

namespace NumberSorter.Core.Logic
{
    public static class AlgorhythmNamer
    {
        private static readonly Dictionary<AlgorhythmType, string> _nameDictionary = new Dictionary<AlgorhythmType, string>();

        static AlgorhythmNamer()
        {
            _nameDictionary.Add(AlgorhythmType.CombSort, "Comb sort");
            _nameDictionary.Add(AlgorhythmType.CycleSort, "Cycle sort");
            _nameDictionary.Add(AlgorhythmType.GnomeSort, "Gnome sort");
            _nameDictionary.Add(AlgorhythmType.BubbleSort, "Bubble sort");
            _nameDictionary.Add(AlgorhythmType.CircleSort, "Circle sort");
            _nameDictionary.Add(AlgorhythmType.SmoothSort, "Smooth sort");
            _nameDictionary.Add(AlgorhythmType.PancakeSort, "Pancake sort");
            _nameDictionary.Add(AlgorhythmType.OddEvenSort, "Odd even sort");
            _nameDictionary.Add(AlgorhythmType.CocktailShakerSort, "Cocktail shaker sort");

            _nameDictionary.Add(AlgorhythmType.TimSortWindowWindow, "Tim Sort (Window sort, Window merge)");
            _nameDictionary.Add(AlgorhythmType.TimSortBinaryInterval, "Tim Sort (Binary sort, Interval merge)");
            _nameDictionary.Add(AlgorhythmType.TimSortInsertionWindow, "Tim Sort (Insertion sort, Window merge)");
            _nameDictionary.Add(AlgorhythmType.TimSortInsertionTripleWindow, "Tim Sort (Insertion sort, Triple window merge)");
            _nameDictionary.Add(AlgorhythmType.TimSortTripleWindowTripleWindow, "Tim Sort (Triple window sort, Triple window merge)");

            _nameDictionary.Add(AlgorhythmType.DequeMergeSort, "Deque merge sort");
            _nameDictionary.Add(AlgorhythmType.WindowMergeSort, "Window merge sort");
            _nameDictionary.Add(AlgorhythmType.IntervalMergeSort, "Interval merge sort");
            _nameDictionary.Add(AlgorhythmType.RecursiveMergeSort, "Recursive merge sort");
            _nameDictionary.Add(AlgorhythmType.HalfInPlaceMergeSort, "Half in place merge sort");
            _nameDictionary.Add(AlgorhythmType.TripleWindowMergeSort, "Triple window merge sort");
            _nameDictionary.Add(AlgorhythmType.KindaInPlaceMergeSort, "Kinda in place merge sort");
            _nameDictionary.Add(AlgorhythmType.WorkAreaInPlaceMergeSort, "Work area in place merge sort");

            _nameDictionary.Add(AlgorhythmType.InsertionSort, "Insertion sort");
            _nameDictionary.Add(AlgorhythmType.BinarySort, "Binary sort");

            _nameDictionary.Add(AlgorhythmType.SelectionSort, "Selection sort");
            _nameDictionary.Add(AlgorhythmType.DoubleSelectionSort, "Double selection sort");

            _nameDictionary.Add(AlgorhythmType.HeapSort, "Heap sort");
            _nameDictionary.Add(AlgorhythmType.JHeapBinarySort, "J heap sort (Binary sort)");
            _nameDictionary.Add(AlgorhythmType.JHeapInsertionSort, "J heap sort (Insertion sort)");

            _nameDictionary.Add(AlgorhythmType.ShellSortCiura, "Shell sort (Ciura)");
            _nameDictionary.Add(AlgorhythmType.ShellSortKnuth, "Shell sort (Knuth)");
            _nameDictionary.Add(AlgorhythmType.ShellSortTokuda, "Shell sort (Tokuda)");

            _nameDictionary.Add(AlgorhythmType.MultiMergeGroupLinearSort, "Multi merge sort (Grouped runs, Linear search)");
            _nameDictionary.Add(AlgorhythmType.MultiMergeSimpleLinearSort, "Multi merge sort (Simple runs, Linear search)");
            _nameDictionary.Add(AlgorhythmType.MultiMergeGroupBinarySort, "Multi merge sort (Grouped runs, Binary search)");
            _nameDictionary.Add(AlgorhythmType.MultiMergeSimpleBinarySort, "Multi merge sort (Simple runs, Binary search)");
            _nameDictionary.Add(AlgorhythmType.MultiMergeGroupBinaryWindowSort, "Multi merge sort (Grouped runs, Binary search, Window merge sort runs)");
            _nameDictionary.Add(AlgorhythmType.MultiMergeGroupBinaryRecursiveSort, "Multi merge sort (Simple runs, Binary search, Recursive)");

            _nameDictionary.Add(AlgorhythmType.KWayMergeSortGroup, "K way merge sort (Grouped runs)");
            _nameDictionary.Add(AlgorhythmType.KWayMergeSortSimple, "K way merge sort (Simple runs)");

            _nameDictionary.Add(AlgorhythmType.IntervalMultiMergeGroupLinearSort, "Interval multi merge sort (Simple runs, Linear search)");
            _nameDictionary.Add(AlgorhythmType.IntervalMultiMergeGroupBinarySort, "Interval multi merge sort (Grouped runs, Binary search)");
            _nameDictionary.Add(AlgorhythmType.IntervalMultiMergeGroupBiasedBinarySort, "Interval multi merge sort (Grouped runs, Biased binary search)");

            _nameDictionary.Add(AlgorhythmType.QuickSortRandomPivot, "Quick sort (Random pivot)");
            _nameDictionary.Add(AlgorhythmType.QuickSortMedianOfThree, "Quick sort (Median of three pivot)");
            _nameDictionary.Add(AlgorhythmType.QuickSortRandomPivotCutoffWindow, "Quick sort (Median of three pivot, Window cutoff)");
            _nameDictionary.Add(AlgorhythmType.QuickSortRandomPivotCutoffInsertion, "Quick sort (Median of three pivot, Insertion cutoff)");
            _nameDictionary.Add(AlgorhythmType.QuickSortRandomPivotCutoffTripleWindow, "Quick sort (Median of three pivot, Triple window cutoff)");

            _nameDictionary.Add(AlgorhythmType.QuickSortLLRandomPivot, "Quick sort LL (Random pivot)");
            _nameDictionary.Add(AlgorhythmType.QuickSortLLMedianOfThree, "Quick sort LL (Median of three pivot)");

            _nameDictionary.Add(AlgorhythmType.BitLSDRadixSort, "Bit LSD radix sort");
            _nameDictionary.Add(AlgorhythmType.BitLSDOptimizedRadixSort, "Bit LSD radix sort (Optimized)");

            _nameDictionary.Add(AlgorhythmType.BitMSDRadixSort, "Bit MSD radix sort");
            _nameDictionary.Add(AlgorhythmType.BitMSDOptimizedRadixSort, "Bit MSD radix sort (Optimized)");

            _nameDictionary.Add(AlgorhythmType.AmericanFlagSort, "American flag sort");
            _nameDictionary.Add(AlgorhythmType.AverageBucketSort, "Average bucket sort");

            _nameDictionary.Add(AlgorhythmType.LSDRadixSortBase2, "LSD radix sort (Base 2, Positive and negative separate)");
            _nameDictionary.Add(AlgorhythmType.LSDRadixSortBase4, "LSD radix sort (Base 4, Positive and negative separate)");
            _nameDictionary.Add(AlgorhythmType.LSDRadixSortBase16, "LSD radix sort (Base 16, Positive and negative separate)");

            _nameDictionary.Add(AlgorhythmType.MSDRadixSortBase2, "MSD radix sort (Base 2, Positive and negative separate)");
            _nameDictionary.Add(AlgorhythmType.MSDRadixSortBase4, "MSD radix sort (Base 4, Positive and negative separate)");
            _nameDictionary.Add(AlgorhythmType.MSDRadixSortBase16, "MSD radix sort (Base 16, Positive and negative separate)");
        }

        public static string GetName(AlgorhythmType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}