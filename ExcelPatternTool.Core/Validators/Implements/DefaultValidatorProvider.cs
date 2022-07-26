using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core.Patterns;

namespace ExcelPatternTool.Core.Validators.Implements
{
    public class DefaultValidatorProvider : ValidatorProvider
    {
        public override IEnumerable<PatternItem> GetPatternItems()
        {
            var result = LocalDataHelper.ReadObjectLocal<Pattern>();
            return result.Patterns;
        }
    }
}
