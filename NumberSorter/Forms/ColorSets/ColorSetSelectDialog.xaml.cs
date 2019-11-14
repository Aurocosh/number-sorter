using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

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

                this.BindCommand(ViewModel, x => x.AddNewCommand, x => x.AddNewContextButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.EditSelectedCommand, x => x.EditSelectedContextButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.DeleteSelectedCommand, x => x.DeleteSelectedContextButton)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
