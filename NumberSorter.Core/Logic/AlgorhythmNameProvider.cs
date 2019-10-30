using System.Collections.Generic;

namespace NumberSorter.Core.Logic
{
    public static class AlgorhythmNamer
    {
        private static readonly Dictionary<AlgorhythmType, string> _nameDictionary = new Dictionary<AlgorhythmType, string>();

        static AlgorhythmNamer()
        {
            _nameDictionary.Add(AlgorhythmType.CombSort, "Comb sort");
            _nameDictionary.Add(AlgorhythmType.HeapSort, "Heap sort");
            _nameDictionary.Add(AlgorhythmType.CycleSort, "Cycle sort");
            _nameDictionary.Add(AlgorhythmType.GnomeSort, "Gnome sort");
            _nameDictionary.Add(AlgorhythmType.BinarySort, "Binary sort");
            _nameDictionary.Add(AlgorhythmType.BubbleSort, "Bubble sort");
            _nameDictionary.Add(AlgorhythmType.CircleSort, "Circle sort");
            _nameDictionary.Add(AlgorhythmType.PancakeSort, "Pancake sort");
            _nameDictionary.Add(AlgorhythmType.OddEvenSort, "Odd even sort");
            _nameDictionary.Add(AlgorhythmType.SelectionSort, "Selection sort");
            _nameDictionary.Add(AlgorhythmType.InsertionSort, "Insertion sort");
            _nameDictionary.Add(AlgorhythmType.DequeMergeSort, "Deque merge sort");
            _nameDictionary.Add(AlgorhythmType.MultiMergeSort, "Multi merge sort");
            _nameDictionary.Add(AlgorhythmType.WindowMergeSort, "Window merge sort");
            _nameDictionary.Add(AlgorhythmType.ShellSortCiura, "Shell sort (Ciura)");
            _nameDictionary.Add(AlgorhythmType.ShellSortKnuth, "Shell sort (Knuth)");
            _nameDictionary.Add(AlgorhythmType.ShellSortTokuda, "Shell sort (Tokuda)");
            _nameDictionary.Add(AlgorhythmType.CocktailShakerSort, "Cocktail shaker sort");
            _nameDictionary.Add(AlgorhythmType.RecursiveMergeSort, "Recursive merge sort");
            _nameDictionary.Add(AlgorhythmType.DoubleSelectionSort, "Double selection sort");
            _nameDictionary.Add(AlgorhythmType.HalfInPlaceMergeSort, "Half in place merge sort");
            _nameDictionary.Add(AlgorhythmType.QuickSortRandomPivot, "Quick sort (Random pivot)");
            _nameDictionary.Add(AlgorhythmType.TripleWindowMergeSort, "Triple window merge sort");
            _nameDictionary.Add(AlgorhythmType.KindaInPlaceMergeSort, "Kinda in place merge sort");
            _nameDictionary.Add(AlgorhythmType.WorkAreaInPlaceMergeSort, "Work area in place merge sort");
            _nameDictionary.Add(AlgorhythmType.WindowWindowTimSort, "Tim Sort (Window sort, Window merge)");
            _nameDictionary.Add(AlgorhythmType.InsertionWindowTimSort, "Tim Sort (Insertion, Window merge)");
            _nameDictionary.Add(AlgorhythmType.QuickSortMedianOfThree, "Quick sort (Median of three pivot)");
            _nameDictionary.Add(AlgorhythmType.GallopingRecursiveMergeSort, "Galloping recursive merge sort");
            _nameDictionary.Add(AlgorhythmType.InsertionTripleWindowTimSort, "Tim Sort (Insertion, Triple window merge)");
            _nameDictionary.Add(AlgorhythmType.TripleWindowTripleWindowTimSort, "Tim Sort (Triple window sort, Triple window merge)");
            _nameDictionary.Add(AlgorhythmType.QuickSortRandomPivotCutoffWindow, "Quick sort (Median of three pivot, Window cutoff)");
            _nameDictionary.Add(AlgorhythmType.QuickSortRandomPivotCutoffInsertion, "Quick sort (Median of three pivot, Insertion cutoff)");
            _nameDictionary.Add(AlgorhythmType.QuickSortRandomPivotCutoffTripleWindow, "Quick sort (Median of three pivot, Triple window cutoff)");
        }

        public static string GetName(AlgorhythmType algorhythmType)
        {
            if (_nameDictionary.TryGetValue(algorhythmType, out string name))
                return name;
            return "Algorhythm name is unknown";
        }
    }
}