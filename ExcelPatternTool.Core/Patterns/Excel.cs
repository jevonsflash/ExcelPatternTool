using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool.Core.Patterns
{
    public class Excel
    {
        public string SheetName { get; set; } = "Sheet1";
        public int SheetNumber { get; set; } = 0;
        public int SkipRow{ get; set; } = 0;

    }
}
