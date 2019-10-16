using Newtonsoft.Json;
using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogMarker<T> : LogAction<T> where T : IEquatable<T>
    {
        [JsonProperty]
        public string MarkerText { get; }

        public LogMarker(int actionIndex, string markerText) : base(actionIndex, LogActionType.LogMarker)
        {
            MarkerText = markerText;
        }

        public override string ToString() => MarkerText;
    }
}
