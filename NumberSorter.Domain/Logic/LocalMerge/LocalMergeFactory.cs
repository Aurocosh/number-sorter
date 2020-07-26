using NumberSorter.Core.Logic.Factories.LocalMerge;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.Sort;
using NumberSorter.Domain.DialogService;
using NumberSorter.Domain.ViewModels;
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
                    return new IntervalMergeFactory(new LinearPositionLocatorFactory());
                case LocalMergeType.IntervalBinarySearch:
                    return new IntervalMergeFactory(new BinaryPositionLocatorFactory());
                case LocalMergeType.IntervalBiasedBinarySearch:
                    return new IntervalMergeFactory(new BiasedBinaryPositionLocatorFactory(8));
                case LocalMergeType.IntervalCustomBiasedBinarySearch:
                    {
                        var viewModel = new BiasValueDialogViewModel();
                        dialogService.ShowModalPresentation(parentViewModel, viewModel);
                        return new IntervalMergeFactory(new BiasedBinaryPositionLocatorFactory(viewModel.BiasValue));
                    }
                case LocalMergeType.Window:
                    return new WindowMergeFactory();
                case LocalMergeType.TripleWindow:
                    return new TripleWindowMergeFactory();

                case LocalMergeType.Buffer:
                    return new BufferMergeFactory(new BiasedBinaryPositionLocatorFactory(8));
                case LocalMergeType.Deque:
                    return new DequeMergeFactory();
                case LocalMergeType.Gallop:
                    return new GallopMergeFactory();
                case LocalMergeType.HalfInPlace:
                    return new HalfInPlaceMergeFactory();
                case LocalMergeType.KindaInPlace:
                    return new KindaInPlaceMergeFactory();
                case LocalMergeType.Rotation:
                    {
                        var position = new BiasedBinaryPositionLocatorFactory(8);
                        var rotation = new RecursiveInPlaceRotationFactory();
                        return new RotationMergeFactory(position, rotation);
                    }
                case LocalMergeType.BufferRotation:
                    {
                        var position = new BiasedBinaryPositionLocatorFactory(8);
                        var rotation = new RecursiveInPlaceRotationFactory();
                        return new BufferRotationMergeFactory(position, rotation);
                    }

                default:
                    return null;
            }
        }
    }
}