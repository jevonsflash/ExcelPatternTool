using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Contracts.Patterns;
using ExcelPatternTool.Contracts;

namespace ExcelPatternTool.StyleMapping
{
    public class StyleMapping
    {
        public StyleMapping()
        {
            Target = Target.Value;//or Formula
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public Target Target { get; set; }


        [JsonConverter(typeof(StringEnumConverter))]
        public Convention Convention { get; set; }

        public string Expression { get; set; }


        

    }
}
