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
using NumberSorter.Domain.DialogService;
using NumberSorter.Core.Logic;
using NumberSorter.Core.Logic.Comparer;
using System.Diagnostics;
using NumberSorter.Domain.Interactions;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Visualizers;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NumberSorter.Domain.Logic;
using NumberSorter.Domain.AppColors;

namespace NumberSorter.Domain.ViewModels
{
    public class VisualizationWindowViewModel : ReactiveObject
    {
        #region Properties

        [Reactive] public bool? DialogResult { get; set; }
        public VisualizationViewModel VisualizationViewModel { get; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> CloseCommand { get; }

        #endregion Commands

        #region Constructors

        public VisualizationWindowViewModel(VisualizationViewModel visualizationViewModel)
        {
            VisualizationViewModel = visualizationViewModel;
            CloseCommand = ReactiveCommand.Create(Close);
        }

        #endregion Constructors

        #region Command functions

        private void Close()
        {
            DialogResult = true;
        }

        #endregion Command functions
    }
}
