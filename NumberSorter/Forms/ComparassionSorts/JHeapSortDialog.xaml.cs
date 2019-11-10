using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class JHeapSortDialog : ReactiveWindow<JHeapSortDialogViewModel>
    {
        public JHeapSortDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.SortTypes, x => x.FinisherComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedFinisherSortType, x => x.FinisherComboBox.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
