using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Linq.Models;
using Workshop.Core.Patterns;

namespace Workshop.Core.Validators
{
    public class ValidateConvention 
    {

        public ValidateConvention()
        {
            
        }

        public ValidateConvention(Func<PatternItem, object, ProcessResult> convention)
        {
            this.Convention = convention;
        }
        public Func<PatternItem, object, ProcessResult> Convention { get; set; }
    }
}
