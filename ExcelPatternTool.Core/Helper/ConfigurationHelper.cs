using ExcelPatternTool.Core.Services;
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
            return AppConfigurtaionService.Configuration==null || string.IsNullOrEmpty(AppConfigurtaionService.Configuration[key]) ? defaultValue : AppConfigurtaionService.Configuration[key];
        }

    }
}
