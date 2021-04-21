using System;
using System.Collections.Generic;
using Workshop.Infrastructure.Models;

namespace Workshop.Core.Validators
{
    public interface IValidatorProvider
    {
        Func<string, string> PropertyTypeMaper { get; set; }
        IEnumerable<ValidatorInfo> GetValidatorInfos();
        ValidateConvention GetConvention(string type);
    }
}