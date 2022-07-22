using System;
using System.Collections.Generic;
using Workshop.Core.Patterns;

namespace Workshop.Core.Validators
{
    public interface IValidatorProvider
    {
        Func<string, string> PropertyTypeMaper { get; set; }
        IEnumerable<PatternItem> GetPatternItems();
        ValidateConvention GetConvention(string type);
    }
}