using NumberSorter.Core.Logic;
using NumberSorter.Domain.Container.Actions.Base;
using ReactiveUI;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class LogActionLineViewModel : ReactiveObject
    {
        public int Index { get; }
        public string Description { get; }
        public LogAction<int> LogAction { get; }

        public LogActionLineViewModel(LogAction<int> logAction)
        {
            LogAction = logAction;
            Index = logAction.ActionIndex;
            Description = logAction.ToString();
        }
    }
}
