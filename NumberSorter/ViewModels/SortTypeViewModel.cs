﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using NumberSorter.Forms;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using System.Windows.Forms;
using NumberSorter.Interactions;
using System.IO;
using System.Text.RegularExpressions;
using DynamicData;
using NumberSorter.Logic;

namespace NumberSorter.ViewModels
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
        public ReactiveCommand<Unit, Unit> TestCommand { get; }

        #endregion Commands


        #region Constructors

        public SortTypeViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);
            TestCommand = ReactiveCommand.Create(Test);

            _sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.BubbleSort, "Bubble sort"));
            _sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.MergeSort, "Merge sort"));
            _sortTypes.Add(new SortTypeLineViewModel(AlgorhythmType.QuickSort, "Quick sort"));

            SelectedSortType = SortTypes.First();
        }

        #endregion Constructors


        #region Command functions

        private void Accept()
        {
            DialogResult = SelectedSortType != null;
        }
        private void Test()
        {
            Console.WriteLine("Test 1");
        }
        #endregion Command functions
    }
}
