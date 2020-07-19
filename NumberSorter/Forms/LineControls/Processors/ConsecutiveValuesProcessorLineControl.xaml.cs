using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class NewConsecutiveListProcessorLineControl : ReactiveUserControl<ConsecutiveValuesProcessorLineViewModel>
    {
        public NewConsecutiveListProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Step, x => x.StepUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.Start, x => x.StartUpDown.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}
