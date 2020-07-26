using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Domain.DialogService;
using ReactiveUI;

namespace NumberSorter.Domain.Logic
{
    public static class PositionLocatorFactory
    {
        public static IPositionLocatorFactory GetPositionLocator(PositionLocatorType type, ReactiveObject parentViewModel, IDialogService<ReactiveObject> dialogService)
        {
            switch (type)
            {
                case PositionLocatorType.Linear:
                    return new LinearPositionLocatorFactory();
                case PositionLocatorType.Binary:
                    return new BinaryPositionLocatorFactory();
                case PositionLocatorType.BiasedBinary:
                    return new BiasedBinaryPositionLocatorFactory(8);
                default:
                    return null;
            }
        }
    }
}