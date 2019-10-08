using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI.Fody.Helpers;
using System.IO;
using System.Text.RegularExpressions;
using NumberSorter.Domain.DialogService;
using NumberSorter.Core.Logic;
using NumberSorter.Core.Logic.Comparer;
using System.Diagnostics;
using NumberSorter.Domain.Interactions;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Visualizers;
using NumberSorter.Domain.Container;

namespace NumberSorter.Domain.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        #region Fields

        private readonly IDialogService<ReactiveObject> _dialogService;
        private IListVisualizer _listVisualizer = new ColumnListVisualizer();

        #endregion Fields

        #region Properties

        [Reactive] public string InputText { get; set; }
        [Reactive] public string OutputText { get; set; }
        [Reactive] public string InfoText { get; set; }
        [Reactive] public string ResultText { get; set; }

        [Reactive] public List<int> InputNumbers { get; set; }
        [Reactive] public SortLog<int> SortingLog { get; set; }

        [Reactive] public WriteableBitmap VisualizationImage { get; set; }

        #endregion Properties

        #region Commands

        public ReactiveCommand<Unit, string> LoadDataCommand { get; }
        public ReactiveCommand<Unit, List<int>> GenerateRandomCommand { get; }
        public ReactiveCommand<Unit, List<int>> GeneratePartiallySortedCommand { get; }
        public ReactiveCommand<Unit, Unit> PerformSortCommand { get; }
        public ReactiveCommand<SizeChangedEventArgs, Unit> ResizeCanvasCommand { get; }

        #endregion Commands


        #region Constructors

        public MainWindowViewModel() : this(null) { }

        public MainWindowViewModel(IDialogService<ReactiveObject> dialogService)
        {
            _dialogService = dialogService;

            InputNumbers = new List<int>();
            SortingLog = new SortLog<int>();

            LoadDataCommand = ReactiveCommand.CreateFromObservable(FindFileToLoad);
            GenerateRandomCommand = ReactiveCommand.CreateFromObservable(GenerateRandom);
            GeneratePartiallySortedCommand = ReactiveCommand.CreateFromObservable(GeneratePartiallySorted);
            PerformSortCommand = ReactiveCommand.Create(SortData);
            ResizeCanvasCommand = ReactiveCommand.Create<SizeChangedEventArgs>(ResizeCanvas);

            VisualizationImage = BitmapFactory.New(700, 480);

            LoadDataCommand
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(LoadNumbersFromFile)
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);

            GenerateRandomCommand
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);
            GeneratePartiallySortedCommand
                .Where(x => x.Count > 0)
                .Subscribe(x => InputNumbers = x);

            this.WhenAnyValue(x => x.InputNumbers)
                .Subscribe(UpdateInputText);

            this.WhenAnyValue(x => x.SortingLog)
                .Subscribe(UpdateOutputText);
        }

        #endregion Constructors

        private void Test()
        {
            // Set the pixel at P(10, 13) to black
            VisualizationImage.SetPixel(10, 13, Colors.Black);

            // Get the color of the pixel at P(30, 43)
            Color color = VisualizationImage.GetPixel(30, 43);

            // Green line from P1(1, 2) to P2(30, 40)
            VisualizationImage.DrawLine(1, 2, 30, 40, Colors.Green);

            // Blue anti-aliased line from P1(10, 20) to P2(50, 70) with a stroke of 5
            VisualizationImage.DrawLineAa(10, 20, 50, 70, Colors.Blue, 5);

            // Black triangle with the points P1(10, 5), P2(20, 40) and P3(30, 10)
            VisualizationImage.DrawTriangle(10, 5, 20, 40, 30, 10, Colors.Black);

            // Red rectangle from the point P1(2, 4) that is 10px wide and 6px high
            VisualizationImage.DrawRectangle(2, 4, 12, 10, Colors.Red);

            // Filled blue ellipse with the center point P1(2, 2) that is 8px wide and 5px high
            VisualizationImage.FillEllipseCentered(2, 2, 8, 5, Colors.Blue);

            // Closed green polyline with P1(10, 5), P2(20, 40), P3(30, 30) and P4(7, 8)
            int[] p = new int[] { 10, 5, 20, 40, 30, 30, 7, 8, 10, 5 };
            VisualizationImage.DrawPolyline(p, Colors.Green);

            // Cubic Beziér curve from P1(5, 5) to P4(20, 7) 
            // with the control points P2(10, 15) and P3(15, 0)
            VisualizationImage.DrawBezier(5, 5, 10, 15, 15, 0, 20, 7, Colors.Purple);

            // Cardinal spline with a tension of 0.5 
            // through the points P1(10, 5), P2(20, 40) and P3(30, 30)
            int[] pts = new int[] { 10, 5, 20, 40, 30, 30 };
            VisualizationImage.DrawCurve(pts, 0.5f, Colors.Yellow);

            // A filled Cardinal spline with a tension of 0.5 
            // through the points P1(10, 5), P2(20, 40) and P3(30, 30) 
            VisualizationImage.FillCurveClosed(pts, 0.5f, Colors.Green);
        }


        #region Command functions

        private IObservable<String> FindFileToLoad()
        {
            return DialogInteractions.FindFileWithType.Handle("Txt files (*.txt)|*.txt");
        }

        private List<int> LoadNumbersFromFile(string filepath)
        {
            var fileText = File.ReadAllText(filepath);
            fileText = Regex.Replace(fileText, @"[^\d\-]", " ");
            fileText = Regex.Replace(fileText, @"\-\s", " ");
            fileText = Regex.Replace(fileText, @"(\d+)\-(\d+)", "%1 -$2");
            fileText = Regex.Replace(fileText, @"\s+", " ");
            fileText = fileText.Trim();

            return fileText.Split(' ')
                .Select(x => int.Parse(x))
                .ToList();
        }

        private IObservable<List<int>> GenerateRandom()
        {
            var viewModel = new NumberGeneratorViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.Numbers);
        }

        private IObservable<List<int>> GeneratePartiallySorted()
        {
            var viewModel = new PartialSortedGeneratorViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);
            return Observable.Return(viewModel.Numbers);
        }

        private void SortData()
        {
            var viewModel = new SortTypeViewModel();
            _dialogService.ShowModalPresentation(this, viewModel);

            if (viewModel.DialogResult != true || viewModel.SelectedSortType == null)
                return;

            var accessTrackingList = new LoggingList<int>(InputNumbers, new IntComparer());

            var algorhythmType = viewModel.SelectedSortType.AlgorhythmType;
            var algorhythm = AlgorhythmFactory.GetAlgorhythm<LogValue<int>>(algorhythmType, accessTrackingList);

            GC.Collect();
            var stopwatch = Stopwatch.StartNew();

            algorhythm.Sort(accessTrackingList);

            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;

            SortingLog = accessTrackingList.GetSortLog(elapsedTime);
        }

        private void ResizeCanvas(SizeChangedEventArgs e)
        {
            var size = e.NewSize;
            int width = (int)size.Width;
            int heigth = (int)size.Height;

            VisualizationImage = BitmapFactory.New(width, heigth);
            _listVisualizer.Redraw(VisualizationImage, InputNumbers);
        }

        #endregion Command functions

        #region Command predicates

        private bool CanAnalizeSeries => true;

        #endregion Command predicates

        private void UpdateInputText(List<int> values)
        {
            InputText = string.Join(", ", values.Select(x => x.ToString()));
            InfoText = $"Input number count: {values.Count}";
            _listVisualizer.Redraw(VisualizationImage, values);
        }

        private void UpdateOutputText(SortLog<int> sortLog)
        {
            OutputText = string.Join(", ", sortLog.FinalState.Select(x => x.ToString()));
            ResultText = sortLog.FullySorted ? $"Write count: {sortLog.TotalWriteCount}\nRead count: {sortLog.TotalReadCount}\nTime: {sortLog.ElapsedTime}" : "";
        }
    }
}
