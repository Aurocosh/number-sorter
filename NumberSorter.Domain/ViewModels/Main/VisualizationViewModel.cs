using DynamicData;
using Newtonsoft.Json;
using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.AppColors;
using NumberSorter.Domain.Audiolizers.Base;
using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Container.Actions.Base;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.Lib;
using NumberSorter.Domain.Logic;
using NumberSorter.Domain.Serialization;
using NumberSorter.Domain.Visualizers;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.ViewModels
{
    public class VisualizationViewModel : ReactiveObject
    {
        #region Fields

        private int _canvasWidth;
        private int _canvasHeigth;

        private int _cacheSize = 100;
        private int _waypointRange = 20;
        private int _waypointCacheSize = 100;
        private int _currentWaypointCount = 0;
        private int _displayedActionCount = 5;

        private readonly JsonFileSerializer _jsonFileSerializer;
        private IListVisualizer _listVisualizer;
        private IStateAudiolizer _stateAudiolizer;

        private readonly SourceList<LogAction<int>> _logActions;
        private readonly IDialogService<ReactiveObject> _dialogService;
        private readonly LimitedDictionary<int, SortState<int>> _sortStateCache;
        private readonly LimitedDictionary<int, SortState<int>> _sortWaypointStateCache;

        private readonly ReadOnlyObservableCollection<LogAction<int>> _filteredLogActions;

        #endregion Fields

        #region Properties
        [Reactive] public bool ElementsDoNotFit { get; set; }
        [Reactive] public int MissingElementCount { get; set; }

        [Reactive] public bool IsAnimating { get; set; }
        [Reactive] public bool ShowActionLog { get; set; }

        [Reactive] public bool ReadActions { get; set; }
        [Reactive] public bool WriteActions { get; set; }
        [Reactive] public bool MarkersActions { get; set; }
        [Reactive] public bool SwapReadActions { get; set; }
        [Reactive] public bool CompareReadActions { get; set; }
        [Reactive] public bool ComparassionActions { get; set; }

        [Reactive] public double AnimationDelay { get; set; }
        [Reactive] public int CurrentActionIndex { get; set; }

        [Reactive] public ColorSet ColorSet { get; set; }
        [Reactive] public string ActionButtonText { get; set; }
        [Reactive] public SortLog<int> SortingLog { get; set; }
        [Reactive] public SortState<int> SortState { get; private set; }
        [Reactive] public WriteableBitmap VisualizationImage { get; private set; }

        public bool CanAnimate { [ObservableAsProperty]get; }
        public int MaxActionIndex { [ObservableAsProperty]get; }
        public int TotalActionCount { [ObservableAsProperty]get; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> PlayPauseCommand { get; }
        public ReactiveCommand<Unit, Unit> ResetCommand { get; }

        public ReactiveCommand<Unit, Unit> GoToStartCommand { get; }
        public ReactiveCommand<Unit, Unit> GoToFinishCommand { get; }

        public ReactiveCommand<Unit, Unit> MinusOneStepCommand { get; }
        public ReactiveCommand<Unit, Unit> PlusOneStepCommand { get; }

        public ReactiveCommand<Unit, Unit> MinusHundredStepsCommand { get; }
        public ReactiveCommand<Unit, Unit> PlusHundredStepsCommand { get; }

        public ReactiveCommand<Unit, Unit> MinusThousandStepsCommand { get; }
        public ReactiveCommand<Unit, Unit> PlusThousandStepsCommand { get; }

        public ReactiveCommand<Unit, Unit> ChangeColorSetCommand { get; }
        public ReactiveCommand<Unit, Unit> ChangeAudoilizerTypeCommand { get; }
        public ReactiveCommand<Unit, Unit> ChangeVisualizationTypeCommand { get; }
        public ReactiveCommand<SizeChangedEventArgs, Unit> ResizeCanvasCommand { get; }

        #endregion Commands

        #region Constructors

        public VisualizationViewModel() : this(null) { }

        public VisualizationViewModel(IDialogService<ReactiveObject> dialogService)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None
            };
            _jsonFileSerializer = new JsonFileSerializer(jsonSerializerSettings);

            _canvasWidth = 700;
            _canvasHeigth = 480;

            _dialogService = dialogService;
            _logActions = new SourceList<LogAction<int>>();
            _sortStateCache = new LimitedDictionary<int, SortState<int>>(_cacheSize);
            _sortWaypointStateCache = new LimitedDictionary<int, SortState<int>>(_waypointCacheSize);

            _listVisualizer = VisualizationFactory.GetVisualizer(VisualizationType.GhostlyColumns);
            _stateAudiolizer = AudiolizerFactory.GetAudiolizer(AudiolizerType.DummyAudiolizer, this, dialogService);

            ElementsDoNotFit = false;
            MissingElementCount = 0;

            IsAnimating = false;
            ShowActionLog = true;

            ReadActions = true;
            WriteActions = true;
            MarkersActions = true;
            SwapReadActions = false;
            CompareReadActions = false;
            ComparassionActions = true;

            AnimationDelay = 0.05f;
            CurrentActionIndex = 0;

            ColorSet = new ColorSet();
            ActionButtonText = "Animate";

            SortingLog = new SortLog<int>();
            VisualizationImage = _listVisualizer.Init(SortingLog, _canvasWidth, _canvasHeigth);
            _stateAudiolizer.Init(SortingLog);

            SortState = SortingLog.InputState;

            var canAnimate = this.WhenAnyValue(x => x.TotalActionCount)
                .Merge(this.WhenAnyValue(x => x.CurrentActionIndex))
                .Select(_ => TotalActionCount > 0 && CurrentActionIndex != MaxActionIndex);

            var isLogSet = this.WhenAnyValue(x => x.TotalActionCount)
                .Select(x => x > 0);

            canAnimate.ToPropertyEx(this, x => x.CanAnimate);

            PlayPauseCommand = ReactiveCommand.Create(PlayOrPause, canAnimate);
            ResetCommand = ReactiveCommand.Create(Reset, isLogSet);

            GoToStartCommand = ReactiveCommand.Create(GoToStart, isLogSet);
            GoToFinishCommand = ReactiveCommand.Create(GoToFinish, isLogSet);

            MinusOneStepCommand = ReactiveCommand.Create(MinusOneStep, isLogSet);
            PlusOneStepCommand = ReactiveCommand.Create(PlusOneStep, isLogSet);

            MinusHundredStepsCommand = ReactiveCommand.Create(MinusHundredSteps, isLogSet);
            PlusHundredStepsCommand = ReactiveCommand.Create(PlusHundredSteps, isLogSet);

            MinusThousandStepsCommand = ReactiveCommand.Create(MinusThousandSteps, isLogSet);
            PlusThousandStepsCommand = ReactiveCommand.Create(PlusThousandSteps, isLogSet);

            ChangeColorSetCommand = ReactiveCommand.Create(ChangeColorSet);
            ChangeAudoilizerTypeCommand = ReactiveCommand.Create(ChangeAudiolizerType);
            ChangeVisualizationTypeCommand = ReactiveCommand.Create(ChangeVisualizationType);
            ResizeCanvasCommand = ReactiveCommand.Create<SizeChangedEventArgs>(ResizeCanvas);

            _logActions.Connect()
                .ObserveOnDispatcher()
                .Bind(out _filteredLogActions)
                .DisposeMany()
                .Subscribe();

            _logActions.Connect()
                .ToCollection()
                .Select(x => Math.Max(x.Count - 1, 0))
                .ToPropertyEx(this, x => x.MaxActionIndex);

            _logActions.Connect()
                .ToCollection()
                .Select(x => Math.Max(x.Count, 0))
                .ToPropertyEx(this, x => x.TotalActionCount);

            this.WhenAnyValue(x => x.IsAnimating)
                .Subscribe(_ => UpdateAnimationStatus());

            this.WhenAnyValue(x => x.SortState)
                .Subscribe(UpdateVisualization);

            this.WhenAnyValue(x => x.CurrentActionIndex)
                .Subscribe(SetActionIndex);

            this.WhenAnyValue(x => x.SortingLog)
                .Subscribe(InitSortLog);

            this.WhenAnyValue(x => x.ReadActions)
                .Subscribe(_ => UpdateActionLog());
            this.WhenAnyValue(x => x.WriteActions)
                .Subscribe(_ => UpdateActionLog());
            this.WhenAnyValue(x => x.MarkersActions)
                .Subscribe(_ => UpdateActionLog());
            this.WhenAnyValue(x => x.SwapReadActions)
                .Subscribe(_ => UpdateActionLog());
            this.WhenAnyValue(x => x.CompareReadActions)
                .Subscribe(_ => UpdateActionLog());
            this.WhenAnyValue(x => x.ComparassionActions)
                .Subscribe(_ => UpdateActionLog());
        }

        #endregion Constructors

        public void PlayOrPauseAnimation()
        {
            if (CanAnimate)
                PlayOrPause();
        }

        #region Command functions

        private void PlayOrPause()
        {
            IsAnimating = !IsAnimating;
        }

        private void Reset()
        {
            IsAnimating = false;
            CurrentActionIndex = 0;
        }

        private void GoToStart() => CurrentActionIndex = 0;
        private void GoToFinish() => CurrentActionIndex = MaxActionIndex;

        private void MinusOneStep() => CurrentActionIndex = ComparableUtility.Clamp(CurrentActionIndex - 1, 0, MaxActionIndex);
        private void PlusOneStep() => CurrentActionIndex = ComparableUtility.Clamp(CurrentActionIndex + 1, 0, MaxActionIndex);

        private void MinusHundredSteps() => CurrentActionIndex = ComparableUtility.Clamp(CurrentActionIndex - 100, 0, MaxActionIndex);
        private void PlusHundredSteps() => CurrentActionIndex = ComparableUtility.Clamp(CurrentActionIndex + 100, 0, MaxActionIndex);

        private void MinusThousandSteps() => CurrentActionIndex = ComparableUtility.Clamp(CurrentActionIndex - 1000, 0, MaxActionIndex);
        private void PlusThousandSteps() => CurrentActionIndex = ComparableUtility.Clamp(CurrentActionIndex + 1000, 0, MaxActionIndex);

        private bool ActionFilter(LogAction<int> logAction)
        {
            switch (logAction.ActionType)
            {
                case LogActionType.LogRead:
                    return ReadActions;
                case LogActionType.LogWrite:
                    return WriteActions;
                case LogActionType.LogMarker:
                    return MarkersActions;
                case LogActionType.LogSwapRead:
                    return SwapReadActions;
                case LogActionType.LogCompareRead:
                    return CompareReadActions;
                case LogActionType.LogComparassion:
                    return ComparassionActions;
            }
            return false;
        }

        private void SetActionIndex(int index)
        {
            if (_filteredLogActions.Count == 0)
                return;
            if (index < 0 || index > MaxActionIndex)
            {
                CurrentActionIndex = ComparableUtility.Clamp(CurrentActionIndex, 0, MaxActionIndex);
                return;
            }
            var action = _filteredLogActions[index];
            SortState = GetState(action.ActionIndex);
        }

        private SortState<int> GetState(int index)
        {
            SortState<int> state = GetCachedState(index);
            if (state != null)
                return state;

            var actionLog = SortingLog.ActionLog;
            if (actionLog.Count == 0)
                return SortingLog.InputState;
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
                state = state.TransformState(action);
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
                return SortingLog.InputState;
            return null;
        }

        private void CacheState(int index, SortState<int> state)
        {
            if (_sortStateCache.ContainsKey(index) || _sortWaypointStateCache.ContainsKey(index))
                return;

            _currentWaypointCount++;
            if (_currentWaypointCount > _waypointRange)
            {
                _currentWaypointCount = 0;
                _sortWaypointStateCache[index] = state;
            }
            else
            {
                _sortStateCache[index] = state;
            }
        }

        private void ChangeColorSet()
        {
            var viewModel = new ColorSetSelectDialogViewModel(_dialogService);
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult != true)
                return;

            ColorSet = viewModel.ColorSet;
            var activeColorSetPath = Path.Combine(FilePaths.AppDataFolder, "ActiveColorSet.json");
            _jsonFileSerializer.SaveToJsonFile(activeColorSetPath, ColorSet);
            UpdateVisualization(SortState);
        }

        private void ChangeAudiolizerType()
        {
            IsAnimating = false;

            var viewModel = new AudiolizerTypeDialogViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult != true || viewModel.SelectedAudiolizerType == null)
                return;

            _stateAudiolizer.Dispose();
            var audiolizerType = viewModel.SelectedAudiolizerType.Type;
            _stateAudiolizer = AudiolizerFactory.GetAudiolizer(audiolizerType, this, _dialogService);
            _stateAudiolizer.Init(SortingLog);
        }

        private void ChangeVisualizationType()
        {
            IsAnimating = false;

            var viewModel = new VisualizationTypeViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult != true || viewModel.SelectedSortType == null)
                return;

            var visualizationType = viewModel.SelectedSortType.VisualizationType;
            _listVisualizer = VisualizationFactory.GetVisualizer(visualizationType);
            _listVisualizer.Init(SortingLog, _canvasWidth, _canvasHeigth);
            UpdateVisualization(SortState);
        }

        private void ResizeCanvas(SizeChangedEventArgs e)
        {
            var size = e.NewSize;
            _canvasWidth = (int)size.Width;
            _canvasHeigth = (int)size.Height;

            VisualizationImage = _listVisualizer.Init(SortingLog, _canvasWidth, _canvasHeigth);
            UpdateVisualization(SortState);
        }

        private void UpdateAnimationStatus()
        {
            if (IsAnimating)
                Task.Run(AnimateSort);
            ActionButtonText = IsAnimating ? "Stop" : "Animate";
        }

        private async Task AnimateSort()
        {
            while (IsAnimating && CurrentActionIndex < MaxActionIndex)
            {
                var delay = TimeSpan.FromSeconds(AnimationDelay);
                await Task.Delay(delay).ConfigureAwait(false);
                Application.Current.Dispatcher.Invoke(() => CurrentActionIndex++);
            }
            Application.Current.Dispatcher.Invoke(() => IsAnimating = false);
        }

        #endregion Command functions

        private void UpdateVisualization(SortState<int> currentListState)
        {
            MissingElementCount = _listVisualizer.Redraw(VisualizationImage, currentListState, ColorSet); ;
            ElementsDoNotFit = MissingElementCount > 0;
            _stateAudiolizer.Play(currentListState);
        }

        private void InitSortLog(SortLog<int> sortLog)
        {
            VisualizationImage = _listVisualizer.Init(sortLog, _canvasWidth, _canvasHeigth);
            _stateAudiolizer.Init(sortLog);
            SortState = SortingLog.InputState;
            UpdateActionLog();
        }

        private void UpdateActionLog()
        {
            _logActions.Clear();
            _sortStateCache.Clear();
            _sortWaypointStateCache.Clear();
            _logActions.AddRange(SortingLog.ActionLog.Where(ActionFilter));
            CurrentActionIndex = 0;
        }
    }
}
