using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Contracts;

namespace ExcelPatternTool.StyleMapping
{
    public class StyleConvention 
    {

        public StyleConvention()
        {

        }

        public StyleConvention(Func<StyleMappingContainer, object, ProcessResult> convention)
        {
            Convention = convention;
        }
        public Func<StyleMappingContainer, object, ProcessResult> Convention { get; set; }
    }
}
