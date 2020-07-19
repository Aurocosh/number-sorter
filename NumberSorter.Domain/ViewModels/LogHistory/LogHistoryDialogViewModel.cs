using System.Linq;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using DynamicData;
using System.Collections.ObjectModel;
using System;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace NumberSorter.Domain.ViewModels
{
    public class LogHistoryDialogViewModel : ReactiveObject
    {
        #region Fields

        private readonly IDialogService<ReactiveObject> _dialogService;
        private readonly JsonFileSerializer _jsonFileSerializer;

        private readonly SourceList<LogGroup> _logGroups;
        private readonly SourceList<LogSummary> _logSummaries;

        private readonly ReadOnlyObservableCollection<LogGroupLineViewModel> _logGroupLineViewModels;
        private readonly ReadOnlyObservableCollection<LogSummaryLineViewModel> _logSummaryLineViewModels;

        #endregion Fields

        #region Properties

        public UnsortedInput<int> InputNumbers { get; private set; }

        [Reactive] public LogGroupLineViewModel SelectedLogGroup { get; set; }
        [Reactive] public LogSummaryLineViewModel SelectedLogSummary { get; set; }

        public ReadOnlyObservableCollection<LogGroupLineViewModel> LogGroupLineViewModels => _logGroupLineViewModels;
        public ReadOnlyObservableCollection<LogSummaryLineViewModel> LogSummaryLineViewModels => _logSummaryLineViewModels;

        [Reactive] public bool? DialogResult { get; set; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, Unit> RemoveSelectedLogGroupCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveOldLogGroupsCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveSelectedLogCommand { get; }

        public ReactiveCommand<Unit, Unit> AcceptCommand { get; }

        #endregion Commands

        #region Constructors

        public LogHistoryDialogViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;
            InputNumbers = new UnsortedInput<int>();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None
            };
            _jsonFileSerializer = new JsonFileSerializer(jsonSerializerSettings);

            _logGroups = new SourceList<LogGroup>();
            _logSummaries = new SourceList<LogSummary>();

            var inputExists = this.WhenAnyValue(x => x.SelectedLogSummary)
                .Select(x => IsInputExists(x?.LogSummary));

            RemoveSelectedLogGroupCommand = ReactiveCommand.Create(RemoveSelectedLogGroup);
            RemoveOldLogGroupsCommand = ReactiveCommand.Create(RemoveOldLogGroups);
            RemoveSelectedLogCommand = ReactiveCommand.Create(RemoveSelectedLogSummary);

            AcceptCommand = ReactiveCommand.Create(Accept, inputExists);

            this.WhenAnyValue(x => x.SelectedLogGroup)
                .Subscribe(UpdateLogSummaries);

            _logGroups
                .Connect()
                .Transform(x => new LogGroupLineViewModel(x))
                .ObserveOnDispatcher()
                .Bind(out _logGroupLineViewModels)
                .DisposeMany()
                .Subscribe();

            _logSummaries
                .Connect()
                .ObserveOnDispatcher()
                .Transform(x => new LogSummaryLineViewModel(x))
                .Bind(out _logSummaryLineViewModels)
                .DisposeMany()
                .Subscribe();

            LoadLogGroups();
        }

        #endregion Constructors

        #region Command functions

        private void RemoveSelectedLogGroup() => _logGroups.Remove(SelectedLogGroup.LogGroup);
        private void RemoveSelectedLogSummary() => _logSummaries.Remove(SelectedLogSummary.LogSummary);
        private void RemoveOldLogGroups() { }

        private void Accept()
        {
            if (SelectedLogSummary != null)
            {
                var logSummary = SelectedLogSummary.LogSummary;
                var inputId = logSummary.InputId.ToString();
                var inputFilePath = Path.Combine(FilePaths.InputsListsFolder, $"{inputId}.json");
                InputNumbers = _jsonFileSerializer.LoadFromJsonFile<UnsortedInput<int>>(inputFilePath);
            }
            DialogResult = true;
        }

        #endregion Command functions

        private bool IsInputExists(LogSummary logSummary)
        {
            if (logSummary == null)
                return false;
            var inputId = logSummary.InputId.ToString();
            var inputFilePath = Path.Combine(FilePaths.InputsListsFolder, $"{inputId}.json");
            return File.Exists(inputFilePath);
        }

        public void LoadLogGroups()
        {
            var logDirectory = FilePaths.LogFolder;
            if (!Directory.Exists(logDirectory))
                return;
            var logPaths = Directory.GetFiles(logDirectory, "*", SearchOption.AllDirectories);
            var logGroups = logPaths
                .Select(x => _jsonFileSerializer.LoadFromJsonFile<LogSummary>(x))
                .GroupBy(x => x.InputId)
                .Select(x => new LogGroup(x.Key, x))
                .ToList();
            _logGroups.AddRange(logGroups);
        }

        public void UpdateLogSummaries(LogGroupLineViewModel logGroupLineView)
        {
            _logSummaries.Clear();
            if (logGroupLineView == null)
                return;

            var logGroup = logGroupLineView.LogGroup;
            var logGroupId = logGroup.Id.ToString();

            var logDirectory = FilePaths.LogFolder;
            var logGroupDirectory = Path.Combine(logDirectory, logGroupId);

            if (!Directory.Exists(logGroupDirectory))
                return;

            var logPaths = Directory.GetFiles(logGroupDirectory);
            var logSummaries = logPaths.Select(x => _jsonFileSerializer.LoadFromJsonFile<LogSummary>(x)).ToList();

            _logSummaries.AddRange(logSummaries);
        }
    }
}
