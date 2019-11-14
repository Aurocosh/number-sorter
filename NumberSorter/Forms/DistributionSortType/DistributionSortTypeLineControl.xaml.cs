using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class DistributionSortTypeLineControl : ReactiveUserControl<DistributionSortTypeLineViewModel>
    {
        public DistributionSortTypeLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                        x => x.Description,
                        x => x.DescriptionTextBlock.Text)
                        .DisposeWith(disposable);
            });
        }
    }
}
