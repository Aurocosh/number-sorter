using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class VisualizationTypeLineControl : ReactiveUserControl<VisualizationTypeLineViewModel>
    {
        public VisualizationTypeLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                        x => x.Name,
                        x => x.DescriptionTextBlock.Text)
                        .DisposeWith(disposable);
            });
        }
    }
}
