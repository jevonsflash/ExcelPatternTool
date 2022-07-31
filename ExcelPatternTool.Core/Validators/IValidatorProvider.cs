using System;
using System.Collections.Generic;
using ExcelPatternTool.Core.Patterns;

namespace ExcelPatternTool.Core.Validators
{
    public interface IValidatorProvider
    {
        Func<string, string> PropertyTypeMaper { get; set; }
        IEnumerable<PatternItem> GetPatternItems();
        ValidateConvention GetConvention(string type);
        Dictionary<string, ValidateConvention> InitConventions();
        object TryGetValue(string varName, object e);
    }
}