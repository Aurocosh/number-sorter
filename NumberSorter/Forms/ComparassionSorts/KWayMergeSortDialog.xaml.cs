using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class KWayMergeSortDialog : ReactiveWindow<KWayMergeSortDialogViewModel>
    {
        public KWayMergeSortDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.KValue, x => x.KValueIntegerUpDown.Value)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.SortTypes, x => x.SortComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedSortType, x => x.SortComboBox.SelectedItem)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.RunLocatorsTypes, x => x.SortRunComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedRunLocatorType, x => x.SortRunComboBox.SelectedItem)
                    .DisposeWith(disposable);

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
