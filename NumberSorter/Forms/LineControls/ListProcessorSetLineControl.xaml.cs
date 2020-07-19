using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    public partial class ListProcessorSetLineControl : ReactiveUserControl<ListProcessorSetViewModel>
    {
        public ListProcessorSetLineControl()
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
