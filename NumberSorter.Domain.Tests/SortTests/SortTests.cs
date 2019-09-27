using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Logic.Algorhythm;
using NumberSorter.Domain.Logic.Comparer;
using System;
using System.Collections.Generic;
using Xunit;

namespace NumberSorter.Domain.Tests.SortTests
{
    public abstract class SortTests
    {
        private IComparer<int> _comparer;
        private readonly ISortAlgorhythm<int> _sort;


        protected SortTests()
        {
            _comparer = new IntComparer();
            _sort = GetAlgorhythm(_comparer);
        }

        protected abstract ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer);

        [Theory]
        [ClassData(typeof(StaticRandom100IntegerListGenerator))]
        public void TestSort100RandomStatic(List<int> testData)
        {
            _sort.Sort(testData);
            bool fullySorted = ListUtility.IsSorted(testData, _comparer);
            Assert.True(fullySorted);
        }

        [Theory]
        [ClassData(typeof(DynamicRandom100IntegerListGenerator))]
        public void TestSort100RandomDynamic(List<int> testData)
        {
            _sort.Sort(testData);
            bool fullySorted = ListUtility.IsSorted(testData, _comparer);
            Assert.True(fullySorted);
        }
    }
}
