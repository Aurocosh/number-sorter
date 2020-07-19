using NumberSorter.Domain.ViewModels;
using ReactiveUI;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class InvertValuesProcessorLineControl : ReactiveUserControl<InvertValuesProcessorLineViewModel>
    {
        public InvertValuesProcessorLineControl()
        {
            InitializeComponent();
            //this.WhenActivated(disposable =>
            //{
            //    this.Bind(ViewModel, x => x.Minimum, x => x.MinSizeUpDown.Value)
            //        .DisposeWith(disposable);
            //    this.Bind(ViewModel, x => x.Maximum, x => x.MaxSizeUpDown.Value)
            //        .DisposeWith(disposable);
            //});
        }
    }
}
