using NumberSorter.Core.CustomGenerators;
using NumberSorter.Domain.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NumberSorter.Domain.ViewModels
{
    public class LogSummaryLineViewModel
    {
        #region Properties

        public DateTime Created => LogSummary.Created;
        public bool FullySorted => LogSummary.FullySorted;
        public float ElapsedTime => LogSummary.ElapsedTime;
        public string AlgorhythmName => LogSummary.AlgorhythmName;

        public string StartId => LogSummary.InputId.ToString();
        public string FinishId => LogSummary.FinishId.ToString();

        public int ElementCount => LogSummary.ElementCount;
        public int TotalReadCount => LogSummary.TotalReadCount;
        public int TotalWriteCount => LogSummary.TotalWriteCount;
        public int TotalComparassionCount => LogSummary.TotalComparassionCount;
        public int TotalActionCount => LogSummary.TotalReadCount + LogSummary.TotalWriteCount + LogSummary.TotalComparassionCount;

        public LogSummary LogSummary { get; }

        #endregion

        public LogSummaryLineViewModel(LogSummary logSummary)
        {
            LogSummary = logSummary;
        }
    }
}
