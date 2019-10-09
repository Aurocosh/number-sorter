using DynamicData;
using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Container.Actions;
using NumberSorter.Domain.Container.Actions.Base;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.Lib;
using NumberSorter.Domain.Visualizers;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.ViewModels
{
    public class VisualizationViewModel : ReactiveObject
    {
        #region Fields

        private int _cacheSize = 60;
        private int _waypointRange = 20;
        private int _waypointCacheSize = 60;

        private IListVisualizer _listVisualizer = new ColumnListVisualizer();

        private readonly SourceList<LogAction<int>> _logActions;
        private readonly IDialogService<ReactiveObject> _dialogService;
        private readonly LimitedDictionary<int, SortState<int>> _sortStateCache;
        private readonly LimitedDictionary<int, SortState<int>> _sortWaypointStateCache;
        private readonly ReadOnlyObservableCollection<LogActionLineViewModel> _logActionViewModels;


        #endregion Fields

        #region Properties

        [Reactive] public int CurrentReads { get; private set; }
        [Reactive] public int CurrentWrites { get; private set; }
        [Reactive] public int CurrentComparassions { get; private set; }

        [Reactive] public bool ReadActions { get; set; }
        [Reactive] public bool WriteActions { get; set; }
        [Reactive] public bool ComparassionActions { get; set; }

        [Reactive] public SortLog<int> SortingLog { get; set; }
        [Reactive] public SortState<int> SortState { get; private set; }

        [Reactive] public int CurrentIndex { get; set; }
        [Reactive] public LogActionLineViewModel CurrentAction { get; set; }
        [Reactive] public WriteableBitmap VisualizationImage { get; private set; }

        public ReadOnlyObservableCollection<LogActionLineViewModel> LogActions => _logActionViewModels;

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> PlayPauseCommand { get; }
        public ReactiveCommand<Unit, Unit> ResetCommand { get; }

        public ReactiveCommand<Unit, Unit> PreviousStepCommand { get; }
        public ReactiveCommand<Unit, Unit> NextStepCommand { get; }

        public ReactiveCommand<SizeChangedEventArgs, Unit> ResizeCanvasCommand { get; }

        #endregion Commands

        #region Constructors

        public VisualizationViewModel() : this(null) { }

        public VisualizationViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;
            _logActions = new SourceList<LogAction<int>>();
            _sortStateCache = new LimitedDictionary<int, SortState<int>>(_cacheSize);
            _sortWaypointStateCache = new LimitedDictionary<int, SortState<int>>(_waypointCacheSize);

            CurrentWrites = 0;
            CurrentReads = 0;
            CurrentComparassions = 0;

            ReadActions = false;
            WriteActions = true;
            ComparassionActions = false;

            SortingLog = new SortLog<int>();
            SortState = SortingLog.StartingState;
            VisualizationImage = BitmapFactory.New(700, 480);

            var actions = _logActions
              .Connect()
              .Filter(ActionFilter)
              .Transform(x => new LogActionLineViewModel(x))
              .ObserveOnDispatcher()
              .Bind(out _logActionViewModels)
              .DisposeMany()
              .Subscribe();

            PlayPauseCommand = ReactiveCommand.Create(PlayOrPause);
            ResetCommand = ReactiveCommand.Create(Reset);
            PreviousStepCommand = ReactiveCommand.Create(PreviousStep);
            NextStepCommand = ReactiveCommand.Create(NextStep);
            ResizeCanvasCommand = ReactiveCommand.Create<SizeChangedEventArgs>(ResizeCanvas);

            this.WhenAnyValue(x => x.SortState)
                .Subscribe(UpdateVisualization);

            this.WhenAnyValue(x => x.CurrentAction)
                .Subscribe(UpdateState);

            this.WhenAnyValue(x => x.SortingLog)
                .Do(x => SortState = x.StartingState)
                .Subscribe(_ => UpdateActionLog());

            this.WhenAnyValue(x => x.ReadActions)
                .Subscribe(_ => UpdateActionLog());
            this.WhenAnyValue(x => x.WriteActions)
                .Subscribe(_ => UpdateActionLog());
            this.WhenAnyValue(x => x.ComparassionActions)
                .Subscribe(_ => UpdateActionLog());
        }

        #endregion Constructors

        #region Command functions

        private void PlayOrPause()
        {
        }

        private void Reset()
        {

        }

        private void PreviousStep() => CurrentIndex = ComparableUtility.Clamp(CurrentIndex - 1, 0, _logActionViewModels.Count);
        private void NextStep() => CurrentIndex = ComparableUtility.Clamp(CurrentIndex + 1, 0, _logActionViewModels.Count);

        private bool ActionFilter(LogAction<int> logAction)
        {
            switch (logAction.ActionType)
            {
                case LogActionType.LogRead:
                    return ReadActions;
                case LogActionType.LogWrite:
                    return WriteActions;
                case LogActionType.LogComparassion:
                    return ComparassionActions;
            }
            return false;
        }

        private void UpdateState(LogActionLineViewModel lineViewModel)
        {
            if (lineViewModel == null)
            {
                SortState = SortingLog.StartingState;
                return;
            }

            int index = lineViewModel.LogAction.ActionIndex;
            SortState = GetState(index);
        }

        private SortState<int> GetState(int index)
        {
            SortState<int> state = GetCachedState(index);
            if (state != null)
                return state;

            var actionLog = SortingLog.ActionLog;
            if (actionLog.Count == 0)
                return SortingLog.StartingState;
            if (index >= SortingLog.ActionLog.Count)
                return SortingLog.FinalState;

            var actionIndex = index;
            var actionsToApply = new List<LogAction<int>>();
            do
            {
                actionsToApply.Add(actionLog[actionIndex--]);
                state = GetCachedState(actionIndex);
            } while (state == null);

            for (int i = actionsToApply.Count - 1; i >= 0; i--)
            {
                var action = actionsToApply[i];
                state = action.TransformState(state);
            }

            CacheState(index, state);
            return state;
        }

        private SortState<int> GetCachedState(int index)
        {
            if (_sortStateCache.TryGetValue(index, out SortState<int> state))
                return state;
            else if (_sortWaypointStateCache.TryGetValue(index, out SortState<int> waypointState))
                return waypointState;
            else if (index < 0)
                return SortingLog.StartingState;
            return null;
        }

        private void CacheState(int index, SortState<int> state)
        {
            if (index % _waypointRange == 0)
                _sortWaypointStateCache[index] = state;
            else
                _sortStateCache[index] = state;
        }

        private void ResizeCanvas(SizeChangedEventArgs e)
        {
            var size = e.NewSize;
            int width = (int)size.Width;
            int heigth = (int)size.Height;

            VisualizationImage = BitmapFactory.New(width, heigth);
            UpdateVisualization(SortState);
        }

        #endregion Command functions

        #region Command predicates

        private bool CanAnalizeSeries => true;

        #endregion Command predicates

        private void UpdateVisualization(SortState<int> currentListState)
        {
            _listVisualizer.Redraw(VisualizationImage, currentListState.State);
        }

        private void UpdateActionLog()
        {
            _logActions.Clear();
            _logActions.AddRange(SortingLog.ActionLog);
        }
    }
}
