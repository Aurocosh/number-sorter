using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

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
                this.Bind(ViewModel,
                    x => x.SelectedListGenerator.Description,
                    x => x.DescriptionTextBox.Text)
                    .DisposeWith(disposable);

                #region ListBox

                this.OneWayBind(ViewModel,
                    x => x.ListGeneratorLineViewModels,
                    x => x.ListGeneratorsList.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    x => x.SelectedListGenerator,
                    x => x.ListGeneratorsList.SelectedItem)
                    .DisposeWith(disposable);

                #endregion

                #region Commands

                this.BindCommand(ViewModel, x => x.AddNewGeneratorCommand, x => x.NewGeneratorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.RemoveSelectedGeneratorCommand, x => x.RemoveSelectedMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.SerializeGeneratorCommand, x => x.SerializeGeneratorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.DeserializeGeneratorCommand, x => x.DeserializeGeneratorMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AddNewGeneratorCommand, x => x.NewGeneratorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.RemoveSelectedGeneratorCommand, x => x.RemoveSelectedContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.SerializeGeneratorCommand, x => x.SerializeGeneratorContextMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);

                #endregion
            });
        }
    }
}
