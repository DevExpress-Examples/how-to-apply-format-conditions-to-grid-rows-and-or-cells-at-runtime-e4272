using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Markup;

namespace Default_MVVM {
    public class ColorToBrushConverter : MarkupExtension, IValueConverter {
        public ColorToBrushConverter() { }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            SolidColorBrush brush = value as SolidColorBrush;
            if(brush != null)
                return brush.Color;
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value is Color)
                return new SolidColorBrush((Color)value);
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
