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

            _nameDictionary.Add(ComparassionAlgorhythmType.TimSort, "Tim Sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.TimSortCustom, "Tim Sort (Custom)");

            _nameDictionary.Add(ComparassionAlgorhythmType.ArrayMergeSort, "Recursive merge sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.DequeMergeSort, "Deque merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.HalfInPlaceMergeSort, "Half in place merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.KindaInPlaceMergeSort, "Kinda in place merge sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.WindowMergeSort, "Window merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.TripleWindowMergeSort, "Triple window merge sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMergeSort, "Interval merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMergeSortCustom, "Interval merge sort (Custom)");

            _nameDictionary.Add(ComparassionAlgorhythmType.DequeBottomUpMergeSort, "Deque merge sort (Bottom up)");
            _nameDictionary.Add(ComparassionAlgorhythmType.HalfInPlaceBottomUpMergeSort, "Half in place merge sort (Bottom up)");
            _nameDictionary.Add(ComparassionAlgorhythmType.KindaInPlaceBottomUpMergeSort, "Kinda in place merge sort (Bottom up)");

            _nameDictionary.Add(ComparassionAlgorhythmType.WindowBottomUpMergeSort, "Window merge sort (Bottom up)");
            _nameDictionary.Add(ComparassionAlgorhythmType.TripleWindowBottomUpMergeSort, "Triple window merge sort (Bottom up)");

            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalBottomUpMergeSort, "Interval merge sort (Bottom up)");
            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalBottomUpMergeSortCustom, "Interval merge sort (Bottom up, Custom)");

            _nameDictionary.Add(ComparassionAlgorhythmType.WorkAreaInPlaceMergeSort, "Work area in place merge sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.InsertionSort, "Insertion sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.BinarySort, "Binary sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.SelectionSort, "Selection sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.DoubleSelectionSort, "Double selection sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.HeapSort, "Heap sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.JHeapSort, "J heap sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.JHeapSortCustom, "J heap sort (Custom)");

            _nameDictionary.Add(ComparassionAlgorhythmType.ShellSort, "Shell sort (Ciura)");
            _nameDictionary.Add(ComparassionAlgorhythmType.ShellSortCustom, "Shell sort (Custom)");

            _nameDictionary.Add(ComparassionAlgorhythmType.KWayMergeSort, "K way merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.KWayMergeSortCustom, "K way merge sort (Custom)");
            _nameDictionary.Add(ComparassionAlgorhythmType.MultiMergeSort, "Multi merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.MultiMergeSortCustom, "Multi merge sort (Custom)");
            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMultiMergeSort, "Interval multi merge sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMultiMergeSortCustom, "Interval multi merge sort (Custom)");

            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSort, "Quick sort LR");
            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortCustom, "Quick sort LR (Custom)");
            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortLL, "Quick sort LL");
            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortLLCustom, "Quick sort LL (Custom)");
            _nameDictionary.Add(ComparassionAlgorhythmType.StableQuickSort, "Stable quick sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.StableQuickSortCustom, "Stable quick sort (Custom)");
            _nameDictionary.Add(ComparassionAlgorhythmType.DualPivotQuickSort, "Dual pivot quick sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.DualPivotQuickSortCustom, "Dual pivot quick sort (Custom)");

            _nameDictionary.Add(ComparassionAlgorhythmType.StrandSort, "Strand sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.DualStrandSort, "Dual strand sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.StrandMergerSort, "Strand sort (Merger)");
            _nameDictionary.Add(ComparassionAlgorhythmType.StrandInPlaceSort, "Strand sort (In place)");
            _nameDictionary.Add(ComparassionAlgorhythmType.StrandFixedBufferSort, "Strand sort (Fixed buffer)");
        }

        public static string GetName(ComparassionAlgorhythmType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}