using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class PartialShuffleValuesProcessorLineControl : ReactiveUserControl<PartialShuffleValuesProcessorLineViewModel>
    {
        public PartialShuffleValuesProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.ShuffleCount, x => x.ShuffleCountUpDown.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}
