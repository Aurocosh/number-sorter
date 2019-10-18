using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Interfaces
{
    public interface IListLogger<T>
    {
        void LogMarker(string markerText);
        void LogRead(int index, LogValue<T> item);
        void LogWrite(int index, LogValue<T> item);
    }
}
