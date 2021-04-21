using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.Models;

namespace Workshop.Core.Validators
{
    public class ValidatorInfo
    {
        private string _description;

        public ValidatorInfo()
        {
            ProcessResult = new ProcessResult();
            ProcessResult.IsValidated = false;
            ProcessResult.Content = Description;
            TargetName = "Value";//or Formula

        }


        public Type EntityType { get; set; }
        public string PropName { get; set; }
        public string TargetName { get; set; }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                ProcessResult.Content = _description;
            }
        }

        public string Convention { get; set; }

        public ProcessResult ProcessResult { get; set; }

        public string Expression { get; set; }
    }
}
