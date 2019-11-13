using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.GapGenerator;
using NumberSorter.Core.Logic.Factories.PivotSelector;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class ComparassionAlgorhythmFactory
    {
        public static ISortFactory GetAlgorhythm(ComparassionAlgorhythmType algorhythmType, ReactiveObject parentViewModel, IDialogService<ReactiveObject> dialogService)
        {
            switch (algorhythmType)
            {
                case ComparassionAlgorhythmType.CombSort:
                    return new CombSortFactory();
                case ComparassionAlgorhythmType.CycleSort:
                    return new CycleSortFactory();
                case ComparassionAlgorhythmType.GnomeSort:
                    return new GnomeSortFactory();
                case ComparassionAlgorhythmType.CircleSort:
                    return new CircleSortFactory();
                case ComparassionAlgorhythmType.SmoothSort:
                    return new SmoothSortFactory();
                case ComparassionAlgorhythmType.BubbleSort:
                    return new BubbleSortFactory();
                case ComparassionAlgorhythmType.PancakeSort:
                    return new PancakeSortFactory();
                case ComparassionAlgorhythmType.OddEvenSort:
                    return new OddEvenSortFactory();
                case ComparassionAlgorhythmType.CocktailShakerSort:
                    return new CocktailShakerSortFactory();

                case ComparassionAlgorhythmType.DequeMergeSort:
                    return new DequeMergeSortFactory();
                case ComparassionAlgorhythmType.WindowMergeSort:
                    return new WindowMergeSortFactory();
                case ComparassionAlgorhythmType.RecursiveMergeSort:
                    return new RecursiveMergeSortFactory();
                case ComparassionAlgorhythmType.HalfInPlaceMergeSort:
                    return new HalfInPlaceMergeSortFactory();
                case ComparassionAlgorhythmType.KindaInPlaceMergeSort:
                    return new KindaInPlaceMergeSortFactory();
                case ComparassionAlgorhythmType.WorkAreaInPlaceMergeSort:
                    return new WorkAreaInPlaceMergeSortFactory();
                case ComparassionAlgorhythmType.IntervalMergeSort:
                    {
                        var viewModel = new IntervalMergeSortDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        var positionLocator = PositionLocatorFactory.GetPositionLocator(viewModel.SelectedPositionLocator.Type, parentViewModel, dialogService);
                        return new IntervalMergeSortFactory(positionLocator);
                    }
                case ComparassionAlgorhythmType.TripleWindowMergeSort:
                    return new TripleWindowMergeSortFactory();

                case ComparassionAlgorhythmType.BinarySort:
                    return new BinarySortFactory();
                case ComparassionAlgorhythmType.InsertionSort:
                    return new InsertionSortFactory();

                case ComparassionAlgorhythmType.SelectionSort:
                    return new SelectionSortFactory();
                case ComparassionAlgorhythmType.DoubleSelectionSort:
                    return new DoubleSelectionSortFactory();

                case ComparassionAlgorhythmType.HeapSort:
                    return new HeapSortFactory();
                case ComparassionAlgorhythmType.JHeapSort:
                    {
                        var viewModel = new JHeapSortDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        var finalSort = GetAlgorhythm(viewModel.SelectedFinisherSortType.Type, parentViewModel, dialogService);
                        return new JHeapSortFactory(finalSort);
                    }
                case ComparassionAlgorhythmType.TimSort:
                    {
                        var viewModel = new TimSortDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        var localMerge = LocalMergeFactory.GetMerge(viewModel.SelectedMergeType.Type, parentViewModel, dialogService);
                        var minrunSort = GetAlgorhythm(viewModel.SelectedSortType.Type, parentViewModel, dialogService);
                        return new TimSortFactory(localMerge, minrunSort);
                    }
                case ComparassionAlgorhythmType.ShellSort:
                    {
                        var viewModel = new ShellSortDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        var gapGenerator = GapGeneratorFactory.GetGapGenerator(viewModel.SelectedGapGenerator.Type);
                        return new ShellSortFactory(gapGenerator);
                    }
                case ComparassionAlgorhythmType.KWayMergeSort:
                    {
                        var viewModel = new KWayMergeSortDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        var sortrunSort = GetAlgorhythm(viewModel.SelectedSortType.Type, parentViewModel, dialogService);
                        var runLocator = RunLocatorFactory.GetLocator(viewModel.SelectedRunLocatorType.Type, parentViewModel, dialogService);
                        var positionLocator = PositionLocatorFactory.GetPositionLocator(viewModel.SelectedPositionLocator.Type, parentViewModel, dialogService);
                        return new KWayMergeSortFactory(viewModel.KValue, sortrunSort, runLocator, positionLocator);
                    }

                case ComparassionAlgorhythmType.MultiMergeSort:
                case ComparassionAlgorhythmType.IntervalMultiMergeSort:
                    {
                        var viewModel = new MultiMergeSortDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        var sortrunSort = GetAlgorhythm(viewModel.SelectedSortType.Type, parentViewModel, dialogService);
                        var runLocator = RunLocatorFactory.GetLocator(viewModel.SelectedRunLocatorType.Type, parentViewModel, dialogService);
                        var positionLocator = PositionLocatorFactory.GetPositionLocator(viewModel.SelectedPositionLocator.Type, parentViewModel, dialogService);

                        switch (algorhythmType)
                        {
                            case ComparassionAlgorhythmType.MultiMergeSort:
                                return new MultiMergeSortFactory(sortrunSort, runLocator, positionLocator);
                            case ComparassionAlgorhythmType.IntervalMultiMergeSort:
                                return new IntervalMultiMergeSortFactory(sortrunSort, runLocator, positionLocator);
                            default:
                                return null;
                        }
                    }

                case ComparassionAlgorhythmType.QuickSort:
                case ComparassionAlgorhythmType.QuickSortLL:
                case ComparassionAlgorhythmType.StableQuickSort:
                case ComparassionAlgorhythmType.DualPivotQuickSort:
                    {
                        var viewModel = new QuickSortDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);

                        var pivotSelector = PivotSelectorFactory.GetPivotSelector(viewModel.SelectedPivotType.Type);
                        var cutoffSort = GetAlgorhythm(viewModel.SelectedCutoffSortType.Type, parentViewModel, dialogService);

                        switch (algorhythmType)
                        {
                            case ComparassionAlgorhythmType.QuickSort:
                                return new QuickSortFactory(viewModel.CutoffValue, cutoffSort, pivotSelector);
                            case ComparassionAlgorhythmType.QuickSortLL:
                                return new QuickSortLLFactory(viewModel.CutoffValue, cutoffSort, pivotSelector);
                            case ComparassionAlgorhythmType.StableQuickSort:
                                return new StableQuickSortFactory(viewModel.CutoffValue, cutoffSort, pivotSelector);
                            case ComparassionAlgorhythmType.DualPivotQuickSort:
                                return new DualPivotQuickSortFactory(viewModel.CutoffValue, cutoffSort, pivotSelector);
                            default:
                                return null;
                        }
                    }

                default:
                    return null;
            }
        }
    }
}