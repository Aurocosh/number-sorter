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
    public class IntervalMergeSortDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<PositionLocatorTypeLineViewModel> _positionLocatorTypes = new SourceList<PositionLocatorTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public PositionLocatorTypeLineViewModel SelectedPositionLocator { get; set; }
        public IEnumerable<PositionLocatorTypeLineViewModel> PositionLocatorTypes => _positionLocatorTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public IntervalMergeSortDialogViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var positionLocatorTypes = EnumUtil.GetValues<PositionLocatorType>();
            var positionLocatorModels = positionLocatorTypes
                .Select(x => new PositionLocatorTypeLineViewModel(x, PositionLocatorNamer.GetName(x)))
                .ToList();
            positionLocatorModels.Sort((x, y) => x.Name.CompareTo(y.Name));
            _positionLocatorTypes.AddRange(positionLocatorModels);

            SelectedPositionLocator = PositionLocatorTypes.First(x => x.Type == PositionLocatorType.BiasedBinary);
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
