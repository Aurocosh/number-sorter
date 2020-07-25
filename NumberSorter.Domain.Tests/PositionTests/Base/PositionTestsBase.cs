using NumberSorter.Core.Logic.Algorhythm.PositionLocator.Base;
using NumberSorter.Core.Logic.Comparer;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using System.Collections.Generic;
using Xunit;

namespace NumberSorter.Domain.Tests.PositionTests.Base
{
    public abstract class PositionTestsBase
    {
        private IComparer<int> _comparer;
        private readonly IPositionLocator<int> _locator;

        protected PositionTestsBase()
        {
            _comparer = new IntComparer();
            _locator = GetAlgorhythm().GetPositionLocator(_comparer);
        }

        protected abstract IPositionLocatorFactory GetAlgorhythm();

        [Theory]
        [InlineData(new object[] { 2, 1, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 } })]
        [InlineData(new object[] { 4, 3, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 } })]
        [InlineData(new object[] { 2, 4, new int[] { 1, 1, 1, 1, 3, 3, 3, 3 } })]
        [InlineData(new object[] { 4, 3, new int[] { 1, 2, 3 } })]
        [InlineData(new object[] { 3, 2, new int[] { 1, 2, 3 } })]
        [InlineData(new object[] { 1, 0, new int[] { 2, 3, 4 } })]
        [InlineData(new object[] { 1, 0, new int[] { 3 } })]
        [InlineData(new object[] { 3, 2, new int[] { 1, 2 } })]
        [InlineData(new object[] { 2, 1, new int[] { 1, 2 } })]
        [InlineData(new object[] { 2, 2, new int[] { 1, 1, 3 } })]
        public void FindFirtsPosition(int value, int firstPosition, int[] input)
        {
            var position = _locator.FindFirstPosition(input, value, 0, input.Length);
            Assert.Equal(firstPosition, position);
        }

        [Theory]
        [InlineData(new object[] { 2, 2, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 } })]
        [InlineData(new object[] { 4, 4, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 } })]
        [InlineData(new object[] { 2, 4, new int[] { 1, 1, 1, 1, 3, 3, 3, 3 } })]
        [InlineData(new object[] { 4, 3, new int[] { 1, 2, 3 } })]
        [InlineData(new object[] { 3, 3, new int[] { 1, 2, 3 } })]
        [InlineData(new object[] { 1, 0, new int[] { 2, 3, 4 } })]
        [InlineData(new object[] { 1, 0, new int[] { 3 } })]
        [InlineData(new object[] { 3, 2, new int[] { 1, 2 } })]
        [InlineData(new object[] { 2, 2, new int[] { 1, 2 } })]
        [InlineData(new object[] { 2, 2, new int[] { 1, 1, 3 } })]
        public void FindLastPosition(int value, int lastPosition, int[] input)
        {
            var position = _locator.FindLastPosition(input, value, 0, input.Length);
            Assert.Equal(lastPosition, position);
        }
    }
}
