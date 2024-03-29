﻿using System;
using System.Collections.Generic;

namespace ExcelPatternTool.Contracts.Validations
{
    public interface IValidatorProvider
    {
        Func<string, string> PropertyTypeMapper { get; set; }
        Dictionary<string,IValidation>  GetValidationContainers(Type entityType);
        IValidateConvention GetConvention(string type);
        Dictionary<string, IValidateConvention> InitConventions();
        object TryGetValue(string varName, object e);
    }
}