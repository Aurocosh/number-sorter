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
    public class DistributionSortTypeViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<DistributionSortTypeLineViewModel> _sortTypes = new SourceList<DistributionSortTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public DistributionSortTypeLineViewModel SelectedSortType { get; set; }
        public IEnumerable<DistributionSortTypeLineViewModel> SortTypes => _sortTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public DistributionSortTypeViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var algorhythmTypes = EnumUtil.GetValues<DistributionAlgorhythmType>();
            var sortTypes = algorhythmTypes
                .Select(x => new DistributionSortTypeLineViewModel(x, DistributionAlgorhythmNamer.GetName(x)))
                .ToList();
            sortTypes.Sort((x, y) => x.Description.CompareTo(y.Description));
            _sortTypes.AddRange(sortTypes);

            SelectedSortType = SortTypes.First();
        }

        #endregion Constructors

        #region Command functions

        private void Accept()
        {
            DialogResult = SelectedSortType != null;
        }
        #endregion Command functions
    }
}
