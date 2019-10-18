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
    public partial class LogHistoryDialog : ReactiveWindow<LogHistoryDialogViewModel>
    {
        public LogHistoryDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    x => x.LogGroupLineViewModels,
                    x => x.LogGroupDataGrid.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    x => x.SelectedLogGroup,
                    x => x.LogGroupDataGrid.SelectedItem)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    x => x.LogSummaryLineViewModels,
                    x => x.LogsDataGrid.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    x => x.SelectedLogSummary,
                    x => x.LogsDataGrid.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.RemoveSelectedLogGroupCommand, x => x.RemoveSelectedLogGroupMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.RemoveOldLogGroupsCommand, x => x.RemoveOldLogGroupsMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.RemoveSelectedLogCommand, x => x.RemoveSelectedLogMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
