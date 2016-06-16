using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Fund.Converters
{
    public class BooleanToBrushConverter : IValueConverter
    {
        public Brush ForTrue { get; set; }
        public Brush ForFalse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return (bool) value ? this.ForTrue : this.ForFalse;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
