using Microsoft.Win32;
using NumberSorter.Domain.Converters;
using NumberSorter.Domain.Interactions;
using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                #region Main binds

                this.OneWayBind(ViewModel, x => x.InputText, x => x.InputTextBox.Text)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.OutputText, x => x.OutputTextBox.Text)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.InfoText, x => x.InfoTextBox.Text)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.ResultText, x => x.ResultTextBox.Text)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.LoadDataCommand, x => x.LoadFromFileButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.GenerateRandomCommand, x => x.GenerateRandomButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.GeneratePartiallySortedCommand, x => x.GeneratePartiallySortedButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.PerformSortCommand, x => x.SortButton)
                    .DisposeWith(disposable);

                #endregion

                #region Visualization binds


                #region Action filters

                this.Bind(ViewModel, x => x.VisualizationViewModel.ReadActions, x => x.ReadsCheck.IsChecked)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.VisualizationViewModel.WriteActions, x => x.WritesCheck.IsChecked)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.VisualizationViewModel.ComparassionActions, x => x.ComparassionsCheck.IsChecked)
                    .DisposeWith(disposable);

                #endregion

                #region Action counters

                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortState.ReadCount, x => x.CurrentReadsLabel.Content)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortState.WriteCount, x => x.CurrentWritesLabel.Content)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortState.ComparassionCount, x => x.CurrentComparesLabel.Content)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortingLog.TotalReadCount, x => x.TotalReadsLabel.Content)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortingLog.TotalWriteCount, x => x.TotalWritesLabel.Content)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortingLog.TotalComparassionCount, x => x.TotalComparesLabel.Content)
                    .DisposeWith(disposable);

                #endregion

                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.VisualizationImage, x => x.VisualizationImage.Source)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.LogActions, x => x.LogActionList.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.AnimationDelay, x => x.AnimationSpeedUpDown.Value)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.VisualizationViewModel.CurrentActionIndex, x => x.ActionIndexUpDown.Value)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.MaxActionIndex, x => x.ActionIndexUpDown.Maximum)
                    .DisposeWith(disposable);

                //this.Bind(ViewModel, x => x.VisualizationViewModel.ShowActionLog, x => x.LogButton.IsChecked)
                //    .DisposeWith(disposable);
                //this.OneWayBind(ViewModel, x => x.VisualizationViewModel.ShowActionLog, x => x.LogActionPanel.Visibility, vmToViewConverterOverride: new VisibilityBindingTypeConverter())
                //    .DisposeWith(disposable);
                //this.OneWayBind(ViewModel, x => x.VisualizationViewModel.ShowActionLog, x => x.LogActionPanelSplitter.Visibility, vmToViewConverterOverride: new VisibilityBindingTypeConverter())
                //    .DisposeWith(disposable);

                #region Commands

                this.BindCommand(ViewModel, x => x.VisualizationViewModel.PlayPauseCommand, x => x.PlayButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.ResetCommand, x => x.ResetButton)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.VisualizationViewModel.GoToStartCommand, x => x.StartButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.GoToFinishCommand, x => x.FinishButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.MinusOneStepCommand, x => x.MinusOneStepButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.PlusOneStepCommand, x => x.PlusOneStepButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.MinusHundredStepsCommand, x => x.MinusHundredStepButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.PlusHundredStepsCommand, x => x.PlusHundredStepButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.MinusThousandStepsCommand, x => x.MinusThousandStepButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.PlusThousandStepsCommand, x => x.PlusThousandStepButton)
                    .DisposeWith(disposable);

                #endregion

                VisualizationCanvas.Events()
                    .SizeChanged
                    .Throttle(TimeSpan.FromSeconds(0.25f))
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .InvokeCommand(this, x => x.ViewModel.VisualizationViewModel.ResizeCanvasCommand)
                    .DisposeWith(disposable);

                #endregion
            });

            DialogInteractions.FindFileWithType.RegisterHandler(context =>
            {
                var dialog = new OpenFileDialog { Filter = context.Input };
                if (dialog.ShowDialog() == true)
                    context.SetOutput(dialog.FileName);
                else
                    context.SetOutput("");
            });
        }
    }
}
