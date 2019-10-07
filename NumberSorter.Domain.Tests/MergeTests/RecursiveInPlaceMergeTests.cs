using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Domain.Tests.MergeTests.Base;

namespace NumberSorter.Domain.Tests.MergeTests
{
    public class RecursiveInPlaceMergeTests : MergeTestsBase
    {
        protected override ILocalMergeAlgothythm<int> GetAlgorhythm(IComparer<int> comparer)
        {
            return new RecursiveInPlaceMerge<int>(comparer);
        }
    }
}
