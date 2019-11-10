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
    public class QuickSortDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<PivotSelectorTypeLineViewModel> _pivotTypes = new SourceList<PivotSelectorTypeLineViewModel>();
        private readonly SourceList<ComparassionSortTypeLineViewModel> _cutoffSortTypes = new SourceList<ComparassionSortTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public int CutoffValue { get; set; }
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public PivotSelectorTypeLineViewModel SelectedPivotType { get; set; }
        [Reactive] public ComparassionSortTypeLineViewModel SelectedCutoffSortType { get; set; }
        public IEnumerable<PivotSelectorTypeLineViewModel> PivotTypes => _pivotTypes.Items;
        public IEnumerable<ComparassionSortTypeLineViewModel> SortTypes => _cutoffSortTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public QuickSortDialogViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            CutoffValue = 16;

            var algorhythmTypes = EnumUtil.GetValues<ComparassionAlgorhythmType>();
            var sortTypes = algorhythmTypes
                .Select(x => new ComparassionSortTypeLineViewModel(x, ComparassionAlgorhythmNamer.GetName(x)))
                .ToList();
            sortTypes.Sort((x, y) => x.Name.CompareTo(y.Name));
            _cutoffSortTypes.AddRange(sortTypes);

            SelectedCutoffSortType = SortTypes.First();

            var pivotTypes = EnumUtil.GetValues<PivotSelectorType>();
            var pivotTypeModels = pivotTypes
                .Select(x => new PivotSelectorTypeLineViewModel(x, PivotSelectorNamer.GetName(x)))
                .ToList();
            pivotTypeModels.Sort((x, y) => x.Name.CompareTo(y.Name));
            _pivotTypes.AddRange(pivotTypeModels);

            SelectedPivotType = PivotTypes.First();
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
