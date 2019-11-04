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
    public partial class IntervalValuesProcessorLineControl : ReactiveUserControl<IntervalValuesProcessorLineViewModel>
    {
        public IntervalValuesProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Normal, x => x.NormalCountUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Inverted, x => x.InvertedCountUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Shuffled, x => x.ShuffledCountUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.ShuffleParts, x => x.ShufflePartsCheckBox.IsChecked)
                    .DisposeWith(disposable);
            });
        }
    }
}
