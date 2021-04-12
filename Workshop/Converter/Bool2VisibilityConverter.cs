using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Workshop.Converter
{
    public class Bool2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (string.IsNullOrEmpty((string)parameter))
            {
                parameter = "true";
            }

            if (bool.Parse((string)parameter))
            {
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;

            }
            else
            {
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
