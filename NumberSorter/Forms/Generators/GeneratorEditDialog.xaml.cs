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

                this.BindCommand(ViewModel, x => x.GenerateCommand, x => x.GenerateButton)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);

                #endregion

                #endregion

                #region List processor

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
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddNewConsequtiveListProcessorCommand, x => x.AddNewConsequtiveListProcessorMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddShuffleValuesProcessorCommand, x => x.AddShuffleValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddRandomizeValuesProcessorCommand, x => x.AddRandomizeValuesProcessorMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.AddDuplicateValuesProcessorProcessorCommand, x => x.AddDuplicateValuesProcessorProcessorMenuItem)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.ClearAllProcessorsCommand, x => x.ClearAllProcessorsMenuItem)
                    .DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ListGenerator.SelectedListProcessorSet.RemoveSelectedProcessorCommand, x => x.RemoveSelectedProcessorMenuItem)
                    .DisposeWith(disposable);

                #endregion

                #endregion
            });
        }
    }
}
