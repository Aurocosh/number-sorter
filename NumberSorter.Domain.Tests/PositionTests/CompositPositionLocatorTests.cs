using NumberSorter.Core.Logic.Factories.PositionLocator;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Domain.Tests.PositionTests.Base;

namespace NumberSorter.Domain.Tests.PositionTests
{
    public class CompositPositionLocatorTests : PositionTestsBase
    {
        protected override IPositionLocatorFactory GetAlgorhythm()
        {
            return new CompositPositionLocatorFactory(16);
        }
    }
}
