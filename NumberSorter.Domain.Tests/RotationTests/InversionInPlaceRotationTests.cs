using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Domain.Tests.RotationTests.Base;

namespace NumberSorter.Domain.Tests.RotationTests
{
    public class InversionInPlaceRotationTests : RotationTestsBase
    {
        protected override ILocalRotationAlgothythm<int> GetAlgorhythm()
        {
            return new InversionInPlaceRotation<int>();
        }
    }
}
