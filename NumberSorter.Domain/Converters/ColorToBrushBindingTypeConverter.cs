using ReactiveUI;
using Splat;
using System;
using System.Windows.Media;

namespace NumberSorter.Domain.Converters
{
    public class ColorToBrushBindingTypeConverter : IBindingTypeConverter, IEnableLogger
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return fromType == typeof(Color) && toType == typeof(Brush) ? 100 : 0;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            try
            {
                var color = (Color)from;
                result = new SolidColorBrush(color);
                return true;
            }
            catch (InvalidCastException error)
            {
                this.Log().Error("Problem converting to Brush", error);
                result = null;
                return false;
            }
        }
    }
}
