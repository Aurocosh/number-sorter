using ReactiveUI;
using Splat;
using System;
using System.Windows;

namespace NumberSorter.Domain.Converters
{
    public class BoolToColumnVisibilityBindingTypeConverter : IBindingTypeConverter, IEnableLogger
    {
        private GridLength VisibleLength { get; }
        private GridLength CollapsedLength { get; }

        public BoolToColumnVisibilityBindingTypeConverter(GridLength visibleLength)
        {
            VisibleLength = visibleLength;
            CollapsedLength = new GridLength(0, GridUnitType.Pixel);
        }

        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return fromType == typeof(bool) && toType == typeof(GridLength) ? 100 : 0;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            try
            {
                result = (bool)from ? VisibleLength : CollapsedLength;
                return true;
            }
            catch (InvalidCastException error)
            {
                this.Log().Error("Problem converting to Grid length", error);
                result = null;
                return false;
            }
        }
    }
}
