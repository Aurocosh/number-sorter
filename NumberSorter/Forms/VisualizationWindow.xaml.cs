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

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VisualizationWindow : ReactiveWindow<VisualizationWindowViewModel>
    {
        public VisualizationWindow()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.VisualizationViewModel.VisualizationImage, x => x.VisualizationImage.Source)
                    .DisposeWith(disposable);

                this.Events()
                    .KeyUp
                    .Where(x => x.Key == Key.Space)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .InvokeCommand(this, x => x.ViewModel.VisualizationViewModel.PlayPauseCommand)
                    .DisposeWith(disposable);

                this.Events()
                    .KeyUp
                    .Where(x => x.Key == Key.Escape)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .InvokeCommand(this, x => x.ViewModel.CloseCommand)
                    .DisposeWith(disposable);
            });
        }
    }
}
