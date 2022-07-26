using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Helper;
using Workshop.Core.Patterns;

namespace Workshop.Core.Validators.Implements
{
    public class DefaultValidatorProvider : ValidatorProvider
    {
        public override IEnumerable<PatternItem> GetPatternItems()
        {
            var result = LocalDataHelper.ReadObjectLocal<Pattern>();
            return result.Patterns;
        }
    }
}
