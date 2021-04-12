using System;
using System.Globalization;
using System.Windows.Data;

namespace Workshop.Converter
{
    public class Enum2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.GetNames(value.GetType());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            var result = Enum.Parse(targetType, value.ToString(), false);
            return result;
        }
    }
}
