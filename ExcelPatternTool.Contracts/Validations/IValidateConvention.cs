using System;
using ExcelPatternTool.Contracts;

namespace ExcelPatternTool.Contracts.Validations
{
    public interface IValidateConvention
    {
        Func<IValidationContainer, object, ProcessResult> Convention { get; set; }
    }
}