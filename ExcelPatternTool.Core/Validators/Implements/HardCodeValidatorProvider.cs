using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ExcelPatternTool.Core.Excel.Core.AdvancedTypes;
using ExcelPatternTool.Core.Linq.Models;
using ExcelPatternTool.Core.Patterns;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core.Linq.Core;

namespace ExcelPatternTool.Core.Validators.Implements
{
    public class HardCodeValidatorProvider : ValidatorProvider
    {
        public override IEnumerable<PatternItem> GetPatternItems()
        {
            var result = new List<PatternItem>();

            var validationPattern = new Validation()
            {
                Description = "需要满足正则表达式",
                Expression = @"^ROUND\(Q\d+\+R\d+\+S\d+\+T\d+\+U\d+\+V\d+\+W\d+\+X\d+\+Y\d+\+Z\d+-AA\d+\+AB\d+\+AC\d+\+AD\d+\+AE\d+-AF\d+\+AG\d+\+AH\d+\+AI\d+-AJ\d+\+AK\d+-AL\d+\+AM\d+,2\)$",
                Convention = Convention.RegularExpression,
                Target = Target.Formula

            };
            result.Add(new PatternItem()
            {
                PropName = "合计",
                HeaderName="Sum",
                Validation=validationPattern
            });


            return result;
        }

    }
}
