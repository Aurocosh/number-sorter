using NumberSorter.ViewModels;
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
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class SortTypeLineControl : ReactiveUserControl<SortTypeLineViewModel>
    {
        public SortTypeLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(ViewModel,
                        x => x.Description,
                        x => x.DescriptionTextBlock.Text)
                        .DisposeWith(disposableRegistration);

                //this.OneWayBind(ViewModel,
                //    viewModel => viewModel.Title,
                //    view => view.titleRun.Text)
                //    .DisposeWith(disposableRegistration);

                //this.OneWayBind(ViewModel,
                //    viewModel => viewModel.Description,
                //    view => view.descriptionRun.Text)
                //    .DisposeWith(disposableRegistration);

                //this.BindCommand(ViewModel,
                //    viewModel => viewModel.OpenPage,
                //    view => view.openButton)
                //    .DisposeWith(disposableRegistration);
            });
        }
    }
}
