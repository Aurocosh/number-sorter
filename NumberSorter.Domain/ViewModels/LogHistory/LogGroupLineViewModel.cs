using NumberSorter.Core.CustomGenerators;
using NumberSorter.Domain.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NumberSorter.Domain.ViewModels
{
    public class LogGroupLineViewModel
    {
        #region Properties

        public int Size => LogGroup.Size;
        public string Id => LogGroup.Id.ToString();
        public LogGroup LogGroup { get; }

        #endregion

        public LogGroupLineViewModel(LogGroup logGroup)
        {
            LogGroup = logGroup;

        }
    }
}
