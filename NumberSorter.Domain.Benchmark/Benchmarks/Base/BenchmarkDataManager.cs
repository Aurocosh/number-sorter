using System;
using System.Collections.Generic;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Base
{
    public class BenchmarkDataManager
    {
        private readonly HashSet<string> _dataNameSet;
        private readonly List<string> _dataNames;
        private readonly List<int[]> _dataSets;
        private readonly List<int[]> _dataSetCopies;
        private readonly List<int> _invalidDataIndexes;

        public BenchmarkDataManager()
        {
            _dataNameSet = new HashSet<string>();
            _dataNames = new List<string>();
            _dataSets = new List<int[]>();
            _dataSetCopies = new List<int[]>();
            _invalidDataIndexes = new List<int>(1);
        }

        public void AddDataSet(string name, int[] dataSet)
        {
            if (_dataNameSet.Contains(name))
                throw new ArgumentException($"Data set {name} already exists");

            _dataNameSet.Add(name);
            _dataNames.Add(name);

            _dataSets.Add(dataSet);

            var dataSetCopy = new int[dataSet.Length];
            Array.Copy(dataSet, dataSetCopy, dataSet.Length);

            _dataSetCopies.Add(dataSetCopy);
        }

        public int[] GetDataSet(int index)
        {
            _invalidDataIndexes.Add(index);
            return _dataSetCopies[index];
        }

        public void Refresh()
        {
            foreach (var index in _invalidDataIndexes)
            {
                var data = _dataSets[index];
                var copy = _dataSetCopies[index];
                Array.Copy(data, copy, data.Length);
            }
            _invalidDataIndexes.Clear();
        }

        public IEnumerable<object[]> GenerateParameters()
        {
            var parameters = new object[_dataNames.Count][];
            for (int i = 0; i < _dataSets.Count; i++)
                parameters[i] = new object[] { _dataNames[i], i };
            return parameters;
        }
    }
}
