using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using NumberSorter.Domain.Visualizers;

namespace NumberSorter.Domain.ViewModels
{
    public class VisualizationTypeViewModel : ReactiveObject
    {
        #region Fields

        private readonly SourceList<VisualizationTypeLineViewModel> _sortTypes = new SourceList<VisualizationTypeLineViewModel>();

        #endregion Fields

        #region Properties
        [Reactive] public bool? DialogResult { get; set; }
        [Reactive] public VisualizationTypeLineViewModel SelectedSortType { get; set; }
        public IEnumerable<VisualizationTypeLineViewModel> SortTypes => _sortTypes.Items;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public VisualizationTypeViewModel()
        {
            AcceptCommand = ReactiveCommand.Create(Accept);

            var sortTypes = new List<VisualizationTypeLineViewModel>();
            sortTypes.Add(new VisualizationTypeLineViewModel(VisualizationType.Columns, "Columns visualizer"));
            sortTypes.Add(new VisualizationTypeLineViewModel(VisualizationType.ColumnsNoSpacers, "Columns visualizer (No spacers)"));
            sortTypes.Add(new VisualizationTypeLineViewModel(VisualizationType.PositiveColumns, "Positive columns visualizer"));
            sortTypes.Add(new VisualizationTypeLineViewModel(VisualizationType.PositiveColumnsNoSpacers, "Positive columns visualizer (No spacer)"));
            sortTypes.Add(new VisualizationTypeLineViewModel(VisualizationType.Points, "Points visualizer"));
            sortTypes.Sort((x, y) => x.Name.CompareTo(y.Name));

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
