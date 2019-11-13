using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Logic;

namespace NumberSorter.Domain.ViewModels
{
    public class ShellSortDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<GapGeneratorTypeLineViewModel> _gapGeneratorTypes = new SourceList<GapGeneratorTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public GapGeneratorTypeLineViewModel SelectedGapGenerator { get; set; }
        public IEnumerable<GapGeneratorTypeLineViewModel> GapGeneratorTypes => _gapGeneratorTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public ShellSortDialogViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var positionLocatorTypes = EnumUtil.GetValues<GapGeneratorType>();
            var positionLocatorModels = positionLocatorTypes
                .Select(x => new GapGeneratorTypeLineViewModel(x, GapGeneratorNamer.GetName(x)))
                .ToList();
            positionLocatorModels.Sort((x, y) => x.Name.CompareTo(y.Name));
            _gapGeneratorTypes.AddRange(positionLocatorModels);

            SelectedGapGenerator = GapGeneratorTypes.First(x => x.Type == GapGeneratorType.Ciura);
        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            DialogResult = true;
        }
        #endregion Command functions
    }
}
