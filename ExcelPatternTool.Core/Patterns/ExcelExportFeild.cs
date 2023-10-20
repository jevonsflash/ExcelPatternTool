using ExcelPatternTool.Contracts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelPatternTool.Core.Patterns
{
    public class ExcelExportFeild
    {
        public int Order { get; set; }

        public string Name { get; set; }


        [JsonConverter(typeof(StringEnumConverter))]
        public FieldValueType Type { get; set; }

        public string Format { get; set; }

        public bool Ignore { get; set; }

        public StylePattern StylePattern { get; set; }
    }
}
