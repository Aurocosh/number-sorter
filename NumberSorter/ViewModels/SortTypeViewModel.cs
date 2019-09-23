using System;
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

namespace NumberSorter.ViewModels
{
    public class SortTypeViewModel : ReactiveObject
    {
        #region Fields

        private SourceList<SortTypeLineViewModel> _sortTypes = new SourceList<SortTypeLineViewModel>();

        #endregion Fields

        #region Properties

        public IEnumerable<SortTypeLineViewModel> SortTypes => _sortTypes.Items;

        #endregion Properties


        #region Commands

        #endregion Commands


        #region Constructors

        public SortTypeViewModel()
        {
            _sortTypes.Add(new SortTypeLineViewModel("Bubble sort"));
            _sortTypes.Add(new SortTypeLineViewModel("Bubble sort"));
            _sortTypes.Add(new SortTypeLineViewModel("Bubble sort"));
        }

        #endregion Constructors


        #region Command functions

        #endregion Command functions
    }
}
