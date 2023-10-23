using System;
using ExcelPatternTool.Contracts;

namespace ExcelPatternTool.Contracts.Validations
{
    public interface IValidateConvention
    {
        Func<string , IValidation, object, ProcessResult> Convention { get; set; }
    }
}