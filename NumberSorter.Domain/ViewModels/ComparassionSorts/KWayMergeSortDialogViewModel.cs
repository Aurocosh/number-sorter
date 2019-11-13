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
    public class KWayMergeSortDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<RunLocatorTypeLineViewModel> _runLocatorTypes = new SourceList<RunLocatorTypeLineViewModel>();
        private readonly SourceList<ComparassionSortTypeLineViewModel> _sortTypes = new SourceList<ComparassionSortTypeLineViewModel>();
        private readonly SourceList<PositionLocatorTypeLineViewModel> _positionLocatorTypes = new SourceList<PositionLocatorTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public int KValue { get; set; }
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public RunLocatorTypeLineViewModel SelectedRunLocatorType { get; set; }
        [Reactive] public ComparassionSortTypeLineViewModel SelectedSortType { get; set; }
        [Reactive] public PositionLocatorTypeLineViewModel SelectedPositionLocator { get; set; }

        public IEnumerable<ComparassionSortTypeLineViewModel> SortTypes => _sortTypes.Items;
        public IEnumerable<RunLocatorTypeLineViewModel> RunLocatorsTypes => _runLocatorTypes.Items;
        public IEnumerable<PositionLocatorTypeLineViewModel> PositionLocatorTypes => _positionLocatorTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public KWayMergeSortDialogViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var algorhythmTypes = EnumUtil.GetValues<ComparassionAlgorhythmType>();
            var sortTypes = algorhythmTypes
                .Select(x => new ComparassionSortTypeLineViewModel(x, ComparassionAlgorhythmNamer.GetName(x)))
                .ToList();
            sortTypes.Sort((x, y) => x.Name.CompareTo(y.Name));
            _sortTypes.AddRange(sortTypes);

            SelectedSortType = SortTypes.First(x => x.Type == ComparassionAlgorhythmType.InsertionSort);

            var runLocatorTypes = EnumUtil.GetValues<RunLocatorType>();
            var runLocatorModels = runLocatorTypes
                .Select(x => new RunLocatorTypeLineViewModel(x, RunLocatorNamer.GetName(x)))
                .ToList();
            runLocatorModels.Sort((x, y) => x.Name.CompareTo(y.Name));
            _runLocatorTypes.AddRange(runLocatorModels);

            SelectedRunLocatorType = RunLocatorsTypes.First(x => x.Type == RunLocatorType.GroupedBinary32);

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
