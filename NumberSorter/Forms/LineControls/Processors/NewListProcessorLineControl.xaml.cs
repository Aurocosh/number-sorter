using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class NewListProcessorLineControl : ReactiveUserControl<NewListProcessorLineViewModel>
    {
        public NewListProcessorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Size, x => x.SizeUpDown.Value)
                    .DisposeWith(disposable);
            });
        }
    }
}
