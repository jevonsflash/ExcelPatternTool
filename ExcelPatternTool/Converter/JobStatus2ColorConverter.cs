using System;
using System.Globalization;
using System.Windows.Data;
using ExcelPatternTool.Model;
using ExcelPatternTool.Model.Enum;

namespace ExcelPatternTool.Converter
{
    public class JobStatus2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = string.Empty;
            if (value == null)
            {
                return color;
            }
            var currentStatus = (JobStatusType)value;

            switch (currentStatus)
            {
                case JobStatusType.Obsolete:
                    color = "Gray";
                    break;
                case JobStatusType.Pending:
                    color = "Gold";
                    break;
                case JobStatusType.Rerunning:
                    color = "Green";
                    break;
                case JobStatusType.Running:
                    color = "Green";
                    break;
                case JobStatusType.Stop:
                    color = "Red";
                    break;
                case JobStatusType.Unspecified:
                    color = "Purple";
                    break;
                default:
                    color = "Blue";
                    break;
            }

            return color;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

