using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class AudiolizerTypeLineControl : ReactiveUserControl<AudiolizerTypeLineViewModel>
    {
        public AudiolizerTypeLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.Name, x => x.DescriptionTextBlock.Text)
                        .DisposeWith(disposable);
            });
        }
    }
}
