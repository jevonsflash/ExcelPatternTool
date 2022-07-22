using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Linq.Models;

namespace Workshop.Core.Patterns
{

    public class ValidationPattern
    {
        private string _description;
        private string _expression;

        public ValidationPattern()
        {
            ProcessResult = new ProcessResult();
            ProcessResult.IsValidated = false;
            ProcessResult.Content = Description + Expression;
            TargetName = "Value";//or Formula

        }


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
