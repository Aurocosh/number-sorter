using Microsoft.Win32;
using NumberSorter.Domain.Interactions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.InputText, x => x.InputTextBox.Text)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.OutputText, x => x.OutputTextBox.Text)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.InfoText, x => x.InfoTextBox.Text)
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.ResultText, x => x.ResultTextBox.Text)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.LoadDataCommand, x => x.LoadFromFileButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.GenerateDataCommand, x => x.GenerateButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.PerformSortCommand, x => x.SortButton)
                    .DisposeWith(disposable);
            });

            DialogInteractions.FindFileWithType.RegisterHandler(context =>
            {
                var dialog = new OpenFileDialog { Filter = context.Input };
                if (dialog.ShowDialog() == true)
                    context.SetOutput(dialog.FileName);
            });
        }
    }
}
