using NumberSorter.Domain.Container;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class LogGroupLineViewModel
    {
        #region Properties

        public int Size => LogGroup.Size;
        public string Name => LogGroup.Name;
        public string Id => LogGroup.Id.ToString();
        public DateTime Created => LogGroup.FirstCreated;

        public LogGroup LogGroup { get; }

        #endregion

        public LogGroupLineViewModel(LogGroup logGroup)
        {
            LogGroup = logGroup;
        }
    }
}
