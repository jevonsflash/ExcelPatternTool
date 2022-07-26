using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Core.Linq.Models;
using ExcelPatternTool.Core.Patterns;

namespace ExcelPatternTool.Core.Validators
{
    public class ValidateConvention 
    {

        public ValidateConvention()
        {
           
        }

        public ValidateConvention(Func<PatternItem, object, ProcessResult> convention)
        {
            this.Convention = convention;
        }
        public Func<PatternItem, object, ProcessResult> Convention { get; set; }
    }
}
