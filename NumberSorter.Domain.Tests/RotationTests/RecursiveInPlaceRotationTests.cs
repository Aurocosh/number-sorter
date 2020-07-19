using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Domain.Tests.RotationTests.Base;

namespace NumberSorter.Domain.Tests.RotationTests
{
    public class RecursiveInPlaceRotationTests : RotationTestsBase
    {
        protected override ILocalRotationAlgothythm<int> GetAlgorhythm()
        {
            return new RecursiveInPlaceRotation<int>();
        }
    }
}
