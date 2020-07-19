using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace NumberSorter.Forms
{
    /// <summary>
    /// Interaction logic for SortTypeDialog.xaml
    /// </summary>
    public partial class ColorSetDialog : ReactiveWindow<ColorSetDialogViewModel>
    {
        public ColorSetDialog()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Name, x => x.NameTextEdit.Text)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.ReadColor, x => x.ReadColorPicker.SelectedColor)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.WriteColor, x => x.WriteColorPicker.SelectedColor)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.NormalColor, x => x.NormalColorPicker.SelectedColor)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.BackgroundColor, x => x.BackgroundColorPicker.SelectedColor)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.CompareEqualColor, x => x.CompareEqualColorPicker.SelectedColor)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.CompareLesserColor, x => x.CompareLesserColorPicker.SelectedColor)
                    .DisposeWith(disposable);
                this.Bind(ViewModel, x => x.CompareBiggerColor, x => x.CompareBiggerColorPicker.SelectedColor)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.AcceptCommand, x => x.AcceptButton)
                    .DisposeWith(disposable);
            });
        }
    }
}
