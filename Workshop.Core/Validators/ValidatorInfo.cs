using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.Models;

namespace Workshop.Core.Validators
{
    public class ValidatorInfo
    {
        public IEnumerable<ValidatorInfoItem> List { get; set; }
    }

    public class ValidatorInfoItem
    {
        private string _description;
        private string _expression;

        public ValidatorInfoItem()
        {
            ProcessResult = new ProcessResult();
            ProcessResult.IsValidated = false;
            ProcessResult.Content = Description + Expression;
            TargetName = "Value";//or Formula

        }


        public string PropName { get; set; }
        public string TargetName { get; set; }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                ProcessResult.Content = _description + _expression;
            }
        }

        public string Convention { get; set; }

        public ProcessResult ProcessResult { get; set; }

        public string Expression
        {
            get => _expression;
            set
            {
                _expression = value;
                ProcessResult.Content = _description + _expression;
            }
        }
    }
}
