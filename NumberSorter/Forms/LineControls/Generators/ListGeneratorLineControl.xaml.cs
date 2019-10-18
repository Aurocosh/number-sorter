using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class ListGeneratorLineControl : ReactiveUserControl<ListGeneratorLineViewModel>
    {
        public ListGeneratorLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.Name, x => x.NameTextBlock.Text)
                    .DisposeWith(disposable);

                //LineGrid.Events()
                //    .MouseDown
                //    .Where(x => x.ClickCount == 2)
                //    .Where(x => x.ChangedButton == MouseButton.Left)
                //    .ObserveOn(RxApp.MainThreadScheduler)
                //    .InvokeCommand(this, x => x.ViewModel.DoubleClickCommand)
                //    .DisposeWith(disposable);
            });
        }
    }
}
