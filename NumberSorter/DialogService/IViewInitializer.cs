using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.DialogService
{
    public interface IViewInitializer
    {
        void Initialize(object view, object viewModel);
    }
}
