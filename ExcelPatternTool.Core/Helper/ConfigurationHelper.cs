using ExcelPatternTool.Core.Excel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool.Core.Helper
{
    public class ConfigurationHelper
    {
        public static string GetConfigValue(string key, string defaultValue = default)
        {
            return AppConfigurtaionService.Configuration==null ? defaultValue : AppConfigurtaionService.Configuration[key];
        }

    }
}
