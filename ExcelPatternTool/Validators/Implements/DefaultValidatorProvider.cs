using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Contracts.Validations;
using ExcelPatternTool.Core.Patterns;
using ExcelPatternTool.Validation;
using ExcelPatternTool.Validation.Helper;

namespace ExcelPatternTool.Validation.Implements
{
    public class DefaultValidatorProvider : ValidatorProvider
    {
        public override IEnumerable<IValidationContainer> GetValidationContainers(Type entityType)
        {
            var result = LocalDataHelper.ReadObjectLocal<Pattern>();
            return result.Patterns;
        }
    }
}
