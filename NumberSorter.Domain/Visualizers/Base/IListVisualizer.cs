using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.Base.Visualizers
{
    public interface IListVisualizer
    {
        void Redraw(WriteableBitmap writeableBitmap, IReadOnlyList<int> list);
    }
}
