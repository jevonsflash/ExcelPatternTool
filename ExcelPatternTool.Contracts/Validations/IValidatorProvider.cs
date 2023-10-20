using System;
using System.Collections.Generic;

namespace ExcelPatternTool.Contracts.Validations
{
    public interface IValidatorProvider
    {
        Func<string, string> PropertyTypeMaper { get; set; }
        IEnumerable<IValidationContainer> GetValidationContainers(Type entityType);
        IValidateConvention GetConvention(string type);
        Dictionary<string, IValidateConvention> InitConventions();
        object TryGetValue(string varName, object e);
    }
}