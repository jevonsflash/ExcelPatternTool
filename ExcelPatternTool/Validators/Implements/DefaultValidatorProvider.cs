using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Common.Helper;
using ExcelPatternTool.Contracts.Validations;
using ExcelPatternTool.Core.Patterns;
using ExcelPatternTool.Validation;

namespace ExcelPatternTool.Validators.Implements
{
    public class DefaultValidatorProvider : ValidatorProvider
    {
        public override Dictionary<string, IValidation> GetValidationContainers(Type entityType)
        {
            var result = LocalDataHelper.ReadObjectLocal<Pattern>();
            return new Dictionary<string, IValidation>(
                result.Patterns.Select(c => new KeyValuePair<string, IValidation>(c.PropName, c.Validation)));
        }
    }
}
