using NumberSorter.Domain.Converters;
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
    /// Interaction logic for SortTypeLineControl.xaml
    /// </summary>
    public partial class ColorSetLineControl : ReactiveUserControl<ColorSetLineViewModel>
    {
        public ColorSetLineControl()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.Name, x => x.NameTextBlock.Text)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.ReadColor, x => x.ReadColorEllipse.Fill, vmToViewConverterOverride: new ColorToBrushBindingTypeConverter())
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.WriteColor, x => x.WriteColorEllipse.Fill, vmToViewConverterOverride: new ColorToBrushBindingTypeConverter())
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.NormalColor, x => x.NormalColorEllipse.Fill, vmToViewConverterOverride: new ColorToBrushBindingTypeConverter())
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.BackgroundColor, x => x.BackgroundColorEllipse.Fill, vmToViewConverterOverride: new ColorToBrushBindingTypeConverter())
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.CompareEqualColor, x => x.EqualColorEllipse.Fill, vmToViewConverterOverride: new ColorToBrushBindingTypeConverter())
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.CompareLesserColor, x => x.LesserColorEllipse.Fill, vmToViewConverterOverride: new ColorToBrushBindingTypeConverter())
                    .DisposeWith(disposable);
                this.OneWayBind(ViewModel, x => x.CompareBiggerColor, x => x.BiggerColorEllipse.Fill, vmToViewConverterOverride: new ColorToBrushBindingTypeConverter())
                    .DisposeWith(disposable);
            });
        }
    }
}
