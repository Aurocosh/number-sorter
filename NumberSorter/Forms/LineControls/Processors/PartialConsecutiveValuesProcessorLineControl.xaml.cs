using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
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
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class PartialConsecutiveValuesProcessorLineControl : ReactiveUserControl<PartialConsecutiveValuesProcessorLineViewModel>
    {
        public PartialConsecutiveValuesProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.StartingIndex, x => x.StartingIndexUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Count, x => x.CountUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.StartingValue, x => x.StartingValueUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Step, x => x.StepUpDown.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}
