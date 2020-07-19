using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class ListGeneratorLineControl : ReactiveUserControl<ListGeneratorLineViewModel>
    {
        public ListGeneratorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Name, x => x.NameTextBlock.Text)
                    .DisposeWith(disposable);
            });
        }
    }
}
