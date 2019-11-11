using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Factories.SortRunLocator;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.ViewModels;
using ReactiveUI;
using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class RunLocatorFactory
    {
        public static ISortRunLocatorFactory GetLocator(RunLocatorType type, ReactiveObject parentViewModel, IDialogService<ReactiveObject> dialogService)
        {
            switch (type)
            {
                case RunLocatorType.Simple:
                    return new SimpleRunLocatorFactory();
                case RunLocatorType.GroupedBinary32:
                    return new GroupingRunLocatorFactory(32, new BinarySortFactory());
                case RunLocatorType.GroupedInsertion32:
                    return new GroupingRunLocatorFactory(32, new InsertionSortFactory());
                case RunLocatorType.GroupedCustom:
                    {
                        var viewModel = new GroupingRunLocatorDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        var sortFactory = ComparassionAlgorhythmFactory.GetAlgorhythm(viewModel.SelectedFinisherSortType.Type, parentViewModel, dialogService);
                        return new GroupingRunLocatorFactory(viewModel.MinrunValue, sortFactory);
                    }
                default:
                    return null;
            }
        }
    }
}