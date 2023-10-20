using ExcelPatternTool.Contracts.Patterns;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool.Core.Patterns
{
    public class DbExport
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TableKeyType TableKeyType { get; set; }
        public string TableName { get; set; }
    }
}
