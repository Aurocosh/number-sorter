using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using NumberSorter.Domain.Logic;

namespace NumberSorter.Domain.ViewModels
{
    public class SortTypeViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<SortTypeLineViewModel> _sortTypes = new SourceList<SortTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public SortTypeLineViewModel SelectedSortType { get; set; }
        public IEnumerable<SortTypeLineViewModel> SortTypes => _sortTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public SortTypeViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var sortTypes = new List<SortTypeLineViewModel>();
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.TimSort, "Tim Sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.HeapSort, "Heap sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.BubbleSort, "Bubble sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.InsertionSort, "Insertion sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.DequeMergeSort, "Deque merge sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.InPlaceMergeSort, "In place merge sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.RecursiveMergeSort, "Recursive merge sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.HalfInPlaceMergeSort, "Half in place merge sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.QuickSortRandomPivot, "Quick sort (Random pivot)"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.KindaInPlaceMergeSort, "Kinda in place merge sort"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.QuickSortMedianOfThree, "Quick sort (Median of three pivot)"));
            sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.GallopingRecursiveMergeSort, "Galloping recursive merge sort"));

            sortTypes.Sort((x, y) => x.Description.CompareTo(y.Description));
            _sortTypes.AddRange(sortTypes);

            SelectedSortType = SortTypes.First();
        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            DialogResult = SelectedSortType != null;
        }
        #endregion Command functions
    }
}
