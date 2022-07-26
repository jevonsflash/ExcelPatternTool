using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelPatternTool.Core.Patterns
{
    public class ExportablePattern
    {
        public int Order { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public string Format { get; set; }

        public bool Ignore { get; set; }

        public StylePattern StylePattern { get; set; }
    }
}
