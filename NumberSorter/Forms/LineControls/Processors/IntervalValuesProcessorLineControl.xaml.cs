using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

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
