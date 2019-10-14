using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Interactions
{
    public static class DialogInteractions
    {
        public static Interaction<string, string> FindFileToOpenWithType { get; } = new Interaction<string, string>();
        public static Interaction<string, string> FindFileToSaveWithType { get; } = new Interaction<string, string>();
    }
}
