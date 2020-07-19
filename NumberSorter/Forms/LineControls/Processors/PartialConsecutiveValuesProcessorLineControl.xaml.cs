using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

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
