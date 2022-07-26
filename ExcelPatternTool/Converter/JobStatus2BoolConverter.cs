using System;
using System.Globalization;
using System.Windows.Data;
using ExcelPatternTool.Model;
using ExcelPatternTool.Model.Enum;

namespace ExcelPatternTool.Converter
{
    public class JobStatus2BoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            JobStatusType type;
            bool result = false;
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

            result = currentStatus == type;
            return result;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

