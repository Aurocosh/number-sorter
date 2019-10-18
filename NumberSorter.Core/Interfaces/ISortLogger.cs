using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Interfaces
{
    public interface ISortLogger<T>
    {
        void LogMarker(string markerText);
        //void LogMarkedInterval(string markerText);
    }
}
