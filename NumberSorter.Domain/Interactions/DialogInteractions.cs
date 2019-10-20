using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NumberSorter.Domain.Interactions
{
    public static class DialogInteractions
    {
        public static Interaction<Color, Color> PickAnotherColor { get; } = new Interaction<Color, Color>();
        public static Interaction<string, string> FindFileToOpenWithType { get; } = new Interaction<string, string>();
        public static Interaction<string, string> FindFileToSaveWithType { get; } = new Interaction<string, string>();
        public static Interaction<YesNoQuestionData, bool> AskYesNoQuestion { get; } = new Interaction<YesNoQuestionData, bool>();
    }
}
