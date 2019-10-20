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
    public partial class ColorSetSelectDialog : ReactiveWindow<ColorSetSelectDialogViewModel>
    {
        public ColorSetSelectDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    x => x.ColorSets,
                    x => x.ColorSetList.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    x => x.SelectedColorSet,
                    x => x.ColorSetList.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AddNewCommand, x => x.AddNewButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.EditSelectedCommand, x => x.EditSelectedButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.DeleteSelectedCommand, x => x.DeleteSelectedButton)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
