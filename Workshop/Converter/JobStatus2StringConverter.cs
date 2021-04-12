using System;
using System.Globalization;
using System.Windows.Data;
using Workshop.Model;

namespace Workshop.Converter
{


    public class JobStatus2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strings = string.Empty;
            string[] strList;
            string result = string.Empty;
            if (value == null)
            {
                return result;
            }
            if (parameter == null)
            {
                strings = "失败|进行中|成功|成功|暂停|未指定|未指定";
            }
            else
            {
                strings = parameter as string;
            }
            strList = strings.Split('|');
            var currentStatus = (JobStatusType)value;

            switch (currentStatus)
            {
                case JobStatusType.Obsolete:
                    result = strList[0];
                    break;
                case JobStatusType.Pending:
                    result = strList[1];
                    break;
                case JobStatusType.Rerunning:
                    result = strList[2];
                    break;
                case JobStatusType.Running:
                    result = strList[3];
                    break;
                case JobStatusType.Stop:
                    result = strList[4];
                    break;
                case JobStatusType.Unspecified:
                    result = strList[5];
                    break;
                default:
                    result = strList[6];
                    break;
            }

            return result;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
