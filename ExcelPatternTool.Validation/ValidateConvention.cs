using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Validations;

namespace ExcelPatternTool.Validation
{
    public class ValidateConvention : IValidateConvention
    {

        public ValidateConvention()
        {

        }

        public ValidateConvention(Func<IValidationContainer, object, ProcessResult> convention)
        {
            Convention = convention;
        }
        public Func<IValidationContainer, object, ProcessResult> Convention { get; set; }
    }
}
