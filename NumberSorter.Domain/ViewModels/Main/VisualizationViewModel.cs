using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.Visualizers;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.ViewModels
{
    public class VisualizationViewModel : ReactiveObject
    {
        #region Fields

        private readonly IDialogService<ReactiveObject> _dialogService;
        private IListVisualizer _listVisualizer = new ColumnListVisualizer();

        #endregion Fields

        #region Properties

        [Reactive] public int CurrentReads { get; private set; }
        [Reactive] public int CurrentWrites { get; private set; }
        [Reactive] public int CurrentComparassions { get; private set; }

        [Reactive] public SortLog<int> SortingLog { get; set; }
        [Reactive] public List<int> CurrentListState { get; private set; }

        [Reactive] public WriteableBitmap VisualizationImage { get; private set; }

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

            CurrentWrites = 0;
            CurrentReads = 0;
            CurrentComparassions = 0;

            SortingLog = new SortLog<int>();
            CurrentListState = new List<int>();
            VisualizationImage = BitmapFactory.New(700, 480);

            PlayPauseCommand = ReactiveCommand.Create(PlayOrPause);
            ResetCommand = ReactiveCommand.Create(Reset);
            PreviousStepCommand = ReactiveCommand.Create(PreviousStep);
            NextStepCommand = ReactiveCommand.Create(NextStep);
            ResizeCanvasCommand = ReactiveCommand.Create<SizeChangedEventArgs>(ResizeCanvas);

            this.WhenAnyValue(x => x.CurrentListState)
                .Subscribe(UpdateVisualization);
        }

        #endregion Constructors

        #region Command functions

        private void PlayOrPause()
        {
        }

        private void Reset()
        {

        }

        private void PreviousStep()
        {

        }

        private void NextStep()
        {

        }

        private void ResizeCanvas(SizeChangedEventArgs e)
        {
            var size = e.NewSize;
            int width = (int)size.Width;
            int heigth = (int)size.Height;

            VisualizationImage = BitmapFactory.New(width, heigth);
            UpdateVisualization(CurrentListState);
        }

        #endregion Command functions

        #region Command predicates

        private bool CanAnalizeSeries => true;

        #endregion Command predicates

        private void UpdateVisualization(List<int> currentListState)
        {
            _listVisualizer.Redraw(VisualizationImage, currentListState);
        }
    }
}
