using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Workshop.Core.Entites;
using Workshop.Core.Excel.Core.AdvancedTypes;
using Workshop.Core.Linq.Models;
using Workshop.Core.Patterns;
using Workshop.Core.Helper;
using Workshop.Core.Linq.Core;

namespace Workshop.Core.Validators.Implements
{
    public class HardCodeValidatorProvider<T> : ValidatorProvider<T>
    {
        public override IEnumerable<PatternItem> GetPatternItems()
        {
            var result = new List<PatternItem>();

            var validationPattern = new ValidationPattern()
            {
                Description = "需要满足正则表达式",
                Expression = @"^ROUND\(Q\d+\+R\d+\+S\d+\+T\d+\+U\d+\+V\d+\+W\d+\+X\d+\+Y\d+\+Z\d+-AA\d+\+AB\d+\+AC\d+\+AD\d+\+AE\d+-AF\d+\+AG\d+\+AH\d+\+AI\d+-AJ\d+\+AK\d+-AL\d+\+AM\d+,2\)$",
                Convention = "DefaultRegular",
                TargetName = "Formula"

            };
            result.Add(new PatternItem()
            {
                PropName = "合计",
                HeaderName="Sum",
                ValidationPattern=validationPattern
            });


            return result;
        }

    }
}
