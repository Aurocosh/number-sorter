using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class IntervalMergeSortDialog : ReactiveWindow<IntervalMergeSortDialogViewModel>
    {
        public IntervalMergeSortDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.PositionLocatorTypes, x => x.PositionLocatorComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedPositionLocator, x => x.PositionLocatorComboBox.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
