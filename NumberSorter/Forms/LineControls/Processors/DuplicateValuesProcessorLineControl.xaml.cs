using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class DuplicateValuesProcessorLineControl : ReactiveUserControl<DuplicateValuesProcessorLineViewModel>
    {
        public DuplicateValuesProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.DuplicateCount, x => x.DuplicatesUpDown.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}
