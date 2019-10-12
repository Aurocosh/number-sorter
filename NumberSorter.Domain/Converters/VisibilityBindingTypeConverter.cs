using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NumberSorter.Domain.Converters
{
    public class VisibilityBindingTypeConverter : IBindingTypeConverter, IEnableLogger
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return fromType == typeof(bool) && toType == typeof(Visibility) ? 100 : 0;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            try
            {
                result = (bool)from ? Visibility.Visible : Visibility.Collapsed;
                return true;
            }
            catch (InvalidCastException error)
            {
                this.Log().Error("Problem converting to HtmlWebViewSource", error);
                result = null;
                return false;
            }
        }
    }
}
