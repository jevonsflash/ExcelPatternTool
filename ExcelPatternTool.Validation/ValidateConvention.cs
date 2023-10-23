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

        public ValidateConvention(Func<string , IValidation, object, ProcessResult> convention)
        {
            Convention = convention;
        }
        public Func<string , IValidation, object, ProcessResult> Convention { get; set; }
    }
}
