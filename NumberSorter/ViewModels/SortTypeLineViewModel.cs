using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.ViewModels
{
    public class SortTypeLineViewModel : ReactiveObject
    {
        private readonly String _name;

        public SortTypeLineViewModel(String name)
        {
            _name = name;
        }

        public string Description => _name;
    }
}
