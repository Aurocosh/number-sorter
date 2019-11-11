using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class TimSortDialog : ReactiveWindow<TimSortDialogViewModel>
    {
        public TimSortDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.SortTypes, x => x.SortComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedSortType, x => x.SortComboBox.SelectedItem)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.MergeTypes, x => x.MergeComboBox.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.SelectedMergeType, x => x.MergeComboBox.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
