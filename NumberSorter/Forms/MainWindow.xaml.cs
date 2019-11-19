using Microsoft.Win32;
using NumberSorter.Domain.Converters;
using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

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
                this.Bind(ViewModel, x => x.Topmost, x => x.Topmost)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.ResizeMode, x => x.ResizeMode)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.WindowState, x => x.WindowState)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.WindowStyle, x => x.WindowStyle)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.ShowControls, x => x.MenuStrip.Visibility, vmToViewConverterOverride: new VisibilityBindingTypeConverter())
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.ShowControls, x => x.ControlPanel.Visibility, vmToViewConverterOverride: new VisibilityBindingTypeConverter())
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.Margin, x => x.MainDockPanel.Margin)
                    .DisposeWith(disposable);

                #region Menu commands

                this.BindCommand(ViewModel, x => x.LoadDataCommand, x => x.LoadFromFileButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.GenerateCustomCommand, x => x.GenerateCustomButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.GenerateRandomCommand, x => x.GenerateRandomButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.GeneratePartiallySortedCommand, x => x.GeneratePartiallySortedButton)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.PerformComparassionSortCommand, x => x.PerformComparassionSortButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.PerformDistributionSortCommand, x => x.PerformDistributionSortButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.SortHistoryCommand, x => x.SortHistoryButton)
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

                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortingLog.Summary.TotalReadCount, x => x.TotalReadsLabel.Content)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortingLog.Summary.TotalWriteCount, x => x.TotalWritesLabel.Content)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.SortingLog.Summary.TotalComparassionCount, x => x.TotalComparesLabel.Content)
                    .DisposeWith(disposable);

                #endregion

                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.VisualizationImage, x => x.VisualizationImage.Source)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.VisualizationViewModel.AnimationDelay, x => x.AnimationSpeedUpDown.Value)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.VisualizationViewModel.CurrentActionIndex, x => x.ActionIndexUpDown.Value)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.MaxActionIndex, x => x.ActionIndexUpDown.Maximum)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.TotalActionCount, x => x.TotalActionsLabel.Content)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.ActionButtonText, x => x.PlayButton.Content)
                    .DisposeWith(disposable);

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

                this.BindCommand(ViewModel, x => x.ToggleControlsCommand, x => x.ToggleControlsMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.ChangeColorSetCommand, x => x.ChangeColorSetMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.ChangeVisualizationTypeCommand, x => x.ChangeVisualizationMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.VisualizationViewModel.ChangeAudoilizerTypeCommand, x => x.ChangeAudiolizerMenuItem)
                    .DisposeWith(disposable);

                #endregion

                VisualizationCanvas.Events()
                    .SizeChanged
                    .Throttle(TimeSpan.FromSeconds(0.25f))
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .InvokeCommand(this, x => x.ViewModel.VisualizationViewModel.ResizeCanvasCommand)
                    .DisposeWith(disposable);

                VisualizationCanvas.Events()
                    .MouseUp
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .InvokeCommand(this, x => x.ViewModel.MouseButtonCommand)
                    .DisposeWith(disposable);

                this.Events()
                    .KeyUp
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .InvokeCommand(this, x => x.ViewModel.KeyPressCommand)
                    .DisposeWith(disposable);

                #endregion
            });
        }
    }
}
