using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Domain.DialogService;
using ReactiveUI;

namespace NumberSorter.Domain.Logic
{
    public static class LocalMergeFactory
    {
        public static ILocalMergeFactory GetMerge(LocalMergeType type, ReactiveObject parentViewModel, IDialogService<ReactiveObject> dialogService)
        {
            switch (type)
            {
                case LocalMergeType.IntervalLinearSearch:
                    return new IntervalMergeSortFactory(new LinearPositionLocatorFactory());
                case LocalMergeType.IntervalBinarySearch:
                    return new IntervalMergeSortFactory(new BinaryPositionLocatorFactory());
                case LocalMergeType.IntervalBiasedBinarySearch:
                    return new IntervalMergeSortFactory(new BiasedBinaryPositionLocatorFactory(2));
                case LocalMergeType.Window:
                    return new WindowMergeSortFactory();
                case LocalMergeType.TripleWindow:
                    return new TripleWindowMergeSortFactory();
                default:
                    return null;
            }
        }
    }
}