using NumberSorter.Core.Logic;
using NumberSorter.Domain.Container.Actions.Base;
using ReactiveUI;
using System;
using System.Windows.Media;

namespace NumberSorter.Domain.ViewModels
{
    public class LogActionLineViewModel : ReactiveObject
    {
        public bool IsCurrent { get; }
        public Color TypeColor { get; }

        public int Index { get; }
        public string Type { get; }
        public string Description { get; }
        public LogAction<int> LogAction { get; }

        public LogActionLineViewModel(bool isCurrent, LogAction<int> logAction)
        {
            IsCurrent = isCurrent;
            LogAction = logAction;

            Index = logAction.ActionIndex;
            Description = logAction.ToString();
            Type = GetTypeName(logAction.ActionType);
            TypeColor = GetTypeColor(logAction.ActionType);
        }

        private static Color GetTypeColor(LogActionType actionType)
        {
            switch (actionType)
            {
                case LogActionType.LogRead:
                    return Color.FromRgb(171, 140, 19);
                case LogActionType.LogWrite:
                    return Color.FromRgb(61, 18, 130);
                case LogActionType.LogComparassion:
                    return Color.FromRgb(19, 171, 22);
                case LogActionType.LogMarker:
                    return Color.FromRgb(252, 98, 3);
            }
            return Color.FromRgb(255, 255, 255);
        }

        private static string GetTypeName(LogActionType actionType)
        {
            switch (actionType)
            {
                case LogActionType.LogRead:
                    return "Read";
                case LogActionType.LogWrite:
                    return "Write";
                case LogActionType.LogComparassion:
                    return "Comp.";
                case LogActionType.LogMarker:
                    return "Mark";
            }
            return "";

        }
    }
}
