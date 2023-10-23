using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Models;

namespace ExcelPatternTool.Core.StyleMapping
{
    public class StyleConvention
    {

        public StyleConvention()
        {

        }

        public StyleConvention(Func<string, StyleMapping, object, StyleMetadata> convention)
        {
            Convention = convention;
        }
        public Func<string, StyleMapping, object, StyleMetadata> Convention { get; set; }
    }
}
