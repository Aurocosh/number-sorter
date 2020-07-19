using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class RandomizeValuesProcessorLineControl : ReactiveUserControl<RandomizeValuesProcessorLineViewModel>
    {
        public RandomizeValuesProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Minimum, x => x.MinSizeUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Maximum, x => x.MaxSizeUpDown.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}
