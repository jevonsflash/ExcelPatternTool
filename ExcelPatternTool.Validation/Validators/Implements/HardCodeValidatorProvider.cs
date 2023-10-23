using System;
using System.Collections.Generic;
using ExcelPatternTool.Contracts.Patterns;
using ExcelPatternTool.Contracts.Validations;

namespace ExcelPatternTool.Validation.Validators.Implements
{
    public class HardCodeValidatorProvider : ValidatorProvider
    {
        public override Dictionary<string, IValidation> GetValidationContainers(Type entityType)
        {
            var result = new Dictionary<string, IValidation>();

            var validationPattern = new Validation()
            {
                Description = "需要满足正则表达式",
                Expression = @"^ROUND\(Q\d+\+R\d+\+S\d+\+T\d+\+U\d+\+V\d+\+W\d+\+X\d+\+Y\d+\+Z\d+-AA\d+\+AB\d+\+AC\d+\+AD\d+\+AE\d+-AF\d+\+AG\d+\+AH\d+\+AI\d+-AJ\d+\+AK\d+-AL\d+\+AM\d+,2\)$",
                Convention = Convention.RegularExpression,
                Target = Target.Formula

            };
            result.Add("合计", validationPattern);
            return result;
        }

    }
}
