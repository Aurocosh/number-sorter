using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class GeneratorEditDialog : ReactiveWindow<GeneratorEditDialogViewModel>
    {
        public GeneratorEditDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                #region Settings

                this.Bind(ViewModel, x => x.ListGenerator.Name, x => x.NameTextBox.Text)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.ListGenerator.Shuffle, x => x.ShuffleCheckBox.IsChecked)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.ListGenerator.Description, x => x.DescriptionTextBox.Text)
                    .DisposeWith(disposable);

                #endregion

                #region List sets

                #region Listbox

                this.OneWayBind(ViewModel,
                    x => x.ListGenerator.ListProcessorSetLineViewModels,
                    x => x.SetsList.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    x => x.ListGenerator.SelectedListProcessorSet,
                    x => x.SetsList.SelectedItem)
                    .DisposeWith(disposable);

                #endregion

                #region Commands

                this.BindCommand(ViewModel, x => x.ListGenerator.AddListProcessorSetCommand, x => x.AddListProcessorSetMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.RemoveSelectedProcessorSetCommand, x => x.RemoveSelectedProcessorSetMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.MoveUpProcessorSetCommand, x => x.MoveUpSelecetedSetMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.MoveDownProcessorSetCommand, x => x.MoveDownSelecetedSetMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ListGenerator.AddListProcessorSetCommand, x => x.AddListProcessorSetContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.RemoveSelectedProcessorSetCommand, x => x.RemoveSelectedProcessorSetContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.MoveUpProcessorSetCommand, x => x.MoveUpSelecetedSetContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.MoveDownProcessorSetCommand, x => x.MoveDownSelecetedSetContextMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);

                #endregion

                #endregion

                #region List processor set

                this.Bind(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.Name, x => x.SetNameTextBox.Text)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.IsSameList, x => x.SetIsSameListCheckBox.IsChecked)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.MinRepeatValue, x => x.SetMinRepeatValue.Value)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.MaxRepeatValue, x => x.SetMaxRepeatValue.Value)
                    .DisposeWith(disposable);

                #region Listbox

                this.OneWayBind(ViewModel,
                    x => x.ListGenerator.SelectedListProcessorSet.ListProcessorViewModels,
                    x => x.ProcessorsList.ItemsSource)
                    .DisposeWith(disposable);
                this.Bind(ViewModel,
                    x => x.ListGenerator.SelectedListProcessorSet.SelectedListProcessor,
                    x => x.ProcessorsList.SelectedItem)
                    .DisposeWith(disposable);

                #endregion

                #region Commands

                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddNewListProcessorCommand, x => x.AddNewListProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddNewVariableListProcessorCommand, x => x.AddNewVariableListProcessorMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddInvertValuesProcessorCommand, x => x.AddInvertValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddShuffleValuesProcessorCommand, x => x.AddShuffleValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddIntervalValuesProcessorCommand, x => x.AddIntervalValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddRandomizeValuesProcessorCommand, x => x.AddRandomizeValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddDuplicateValuesProcessorCommand, x => x.AddDuplicateValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddConsequtiveValuesProcessorCommand, x => x.AddConsequtiveValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddPartialShuffleValuesProcessorCommand, x => x.AddPartialShuffleValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddPartialConsecutiveValuesProcessorCommand, x => x.AddPartialConsecutiveValuesProcessorMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.ClearAllProcessorsCommand, x => x.ClearAllProcessorsMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.RemoveSelectedProcessorCommand, x => x.RemoveSelectedProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.MoveUpSelectedProcessorCommand, x => x.MoveUpSelectedProcessorMenuItem)
                     .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.MoveDownSelectedProcessorCommand, x => x.MoveDownSelectedProcessorMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddNewListProcessorCommand, x => x.AddNewListProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddNewVariableListProcessorCommand, x => x.AddNewVariableListProcessorContextMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddInvertValuesProcessorCommand, x => x.AddInvertValuesProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddShuffleValuesProcessorCommand, x => x.AddShuffleValuesProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddIntervalValuesProcessorCommand, x => x.AddIntervalValuesProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddRandomizeValuesProcessorCommand, x => x.AddRandomizeValuesProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddDuplicateValuesProcessorCommand, x => x.AddDuplicateValuesProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddConsequtiveValuesProcessorCommand, x => x.AddConsequtiveValuesProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddPartialShuffleValuesProcessorCommand, x => x.AddPartialShuffleValuesProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddPartialConsecutiveValuesProcessorCommand, x => x.AddPartialConsecutiveValuesProcessorContextMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.RemoveSelectedProcessorCommand, x => x.RemoveSelectedProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.MoveUpSelectedProcessorCommand, x => x.MoveUpSelectedProcessorContextMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.MoveDownSelectedProcessorCommand, x => x.MoveDownSelectedProcessorContextMenuItem)
                    .DisposeWith(disposable);

                #endregion

                #endregion
            });
        }
    }
}
