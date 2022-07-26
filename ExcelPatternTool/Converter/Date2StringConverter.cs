using System;
using System.Globalization;
using System.Windows.Data;

namespace ExcelPatternTool.Converter
{
	public class Date2StringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
		    if (parameter==null || !bool.Parse(parameter.ToString()))
		    {
		        return DatetimeToString((DateTime) value);

		    }
		    else
		    {
                var sdate = (DateTime) value;
		        return  sdate.ToString("hh:mm");
            }
        }

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public string DatetimeToString(DateTime sdate)
		{
			string result;
			if (sdate != DateTime.MinValue)
			{
				if (sdate.Date == DateTime.Today) { result = "今天" + sdate.ToString("T"); }
				else { result = sdate.ToString("MM-dd hh:mm:ss"); }
			}
			else
			{
				result = "无记录";
			}
			return result;
		}
	}
}