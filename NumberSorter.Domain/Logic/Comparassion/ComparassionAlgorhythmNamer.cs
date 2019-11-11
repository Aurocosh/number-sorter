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
            _nameDictionary.Add(ComparassionAlgorhythmType.JHeapSort, "J heap sort");

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

            _nameDictionary.Add(ComparassionAlgorhythmType.IntervalMultiMergeSort, "Interval multi merge sort");

            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSort, "Quick sort LR");
            _nameDictionary.Add(ComparassionAlgorhythmType.QuickSortLL, "Quick sort LL");
            _nameDictionary.Add(ComparassionAlgorhythmType.StableQuickSort, "Stable quick sort");
            _nameDictionary.Add(ComparassionAlgorhythmType.DualPivotQuickSort, "Dual pivot quick sort");
        }

        public static string GetName(ComparassionAlgorhythmType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}