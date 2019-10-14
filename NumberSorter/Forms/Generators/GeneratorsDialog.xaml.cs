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
    public partial class GeneratorsDialog : ReactiveWindow<GeneratorsDialogViewModel>
    {
        public GeneratorsDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    x => x.ListGeneratorLineViewModels,
                    x => x.ListGeneratorsList.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    x => x.SelectedListGenerator,
                    x => x.ListGeneratorsList.SelectedItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AddNewGeneratorCommand, x => x.NewGeneratorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.RemoveSelectedGeneratorCommand, x => x.RemoveSelectedMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.SerializeGeneratorCommand, x => x.SerializeGeneratorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.DeserializeGeneratorCommand, x => x.DeserializeGeneratorMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.GenerateCommand, x => x.GenerateButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
