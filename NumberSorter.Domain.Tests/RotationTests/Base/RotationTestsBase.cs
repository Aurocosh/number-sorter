using AutoFixture.Xunit2;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Comparer;
using NumberSorter.Domain.Tests.RotationTests.Base.IntegerGenerators.Dynamic;
using System;
using System.Collections.Generic;
using Xunit;

namespace NumberSorter.Domain.Tests.RotationTests.Base
{
    public abstract class RotationTestsBase
    {
        private IComparer<int> _comparer;
        private readonly ILocalRotationAlgothythm<int> _rotation;

        protected RotationTestsBase()
        {
            _comparer = new IntComparer();
            _rotation = GetAlgorhythm();
        }

        protected abstract ILocalRotationAlgothythm<int> GetAlgorhythm();

        [Theory]
        [ClassData(typeof(RotationTest_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator))]
        public void RotateTwoSortedParts_RandomArray(List<int> input, SortRun firstRun, SortRun secondRun)
        {
            var result = new List<int>(input);
            _rotation.Rotate(result, firstRun, secondRun);
            bool fullySorted = ListUtility.IsSorted(result, _comparer);
            var message = GetResultMessage(fullySorted, input, result, firstRun, secondRun);
            Assert.True(fullySorted, message);
        }

        private static string GetResultMessage(bool isFullySorted, IList<int> input, IList<int> result, SortRun firstRun, SortRun secondRun)
        {
            if (isFullySorted)
                return "";
            var inputString = string.Join("\t", input);
            var resultString = string.Join("\t", result);
            return $"Failed to rotate list:\nFirst run: {firstRun}\nSecond run: {secondRun}\nInput: {inputString}\nResult: {resultString}";
        }
    }
}
