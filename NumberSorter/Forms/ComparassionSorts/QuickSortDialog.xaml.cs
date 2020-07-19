using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class QuickSortDialog : ReactiveWindow<QuickSortDialogViewModel>
    {
        public QuickSortDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.CutoffValue, x => x.CutoffIntegerUpDown.Value)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.SortTypes, x => x.CutoffComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedCutoffSortType, x => x.CutoffComboBox.SelectedItem)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.PivotTypes, x => x.PivotComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedPivotType, x => x.PivotComboBox.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
