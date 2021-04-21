using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.Models;

namespace Workshop.Core.Validators
{
    public class ValidateConvention 
    {

        public ValidateConvention()
        {
            
        }

        public ValidateConvention(Func<ValidatorInfo, object, ProcessResult> convention)
        {
            this.Convention = convention;
        }
        public Func<ValidatorInfo, object, ProcessResult> Convention { get; set; }
    }
}
