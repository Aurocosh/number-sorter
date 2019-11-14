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
    public partial class ListProcessorSetLineControl : ReactiveUserControl<ListProcessorSetViewModel>
    {
        public ListProcessorSetLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Name, x => x.NameTextBlock.Text)
                        .DisposeWith(disposable);
            });
        }
    }
}
