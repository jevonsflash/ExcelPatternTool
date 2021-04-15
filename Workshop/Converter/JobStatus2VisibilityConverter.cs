using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Workshop.Model;
using Workshop.Model.Enum;

namespace Workshop.Converter
{
    public class JobStatus2VisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            JobStatusType type;
            Visibility result = Visibility.Collapsed;
            if (value == null)
            {
                return result;
            }

            if (parameter == null)
            {
                type = JobStatusType.Unspecified;
            }
            else
            {
                type = (JobStatusType)Enum.Parse(typeof(JobStatusType), parameter as string, false);
            }


            var currentStatus = (JobStatusType)value;

            if (currentStatus == type)
            {
                result = Visibility.Visible;
            }
            else
            {
                result = Visibility.Collapsed;
            }
            return result;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

