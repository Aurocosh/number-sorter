using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using System.IO;
using System.Text.RegularExpressions;
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

            _sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.BubbleSort, "Bubble sort"));
            _sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.InsertionSort, "Insertion sort"));
            _sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.RecursiveMergeSort, "Recursive merge sort"));
            _sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.QuickSortRandomPivot, "Quick sort (Random pivot)"));
            _sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.QuickSortMedianOfThree, "Quick sort (Median of three pivot)"));

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
