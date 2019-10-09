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
    /// Interaction logic for LogActionLineControl.xaml
    /// </summary>
    public partial class LogActionLineControl : ReactiveUserControl<LogActionLineViewModel>
    {
        public LogActionLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                        x => x.Index,
                        x => x.IndexTextBlock.Text)
                        .DisposeWith(disposable);
                this.OneWayBind(ViewModel,
                        x => x.Description,
                        x => x.DescriptionTextBlock.Text)
                        .DisposeWith(disposable);
            });
        }
    }
}
