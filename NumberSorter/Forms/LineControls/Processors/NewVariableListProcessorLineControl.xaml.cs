using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class NewVariableListProcessorLineControl : ReactiveUserControl<NewVariableListProcessorLineViewModel>
    {
        public NewVariableListProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.MinSize, x => x.MinSizeUpDown.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.MaxSize, x => x.MaxSizeUpDown.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}
