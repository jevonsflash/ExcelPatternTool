using ExcelPatternTool.Contracts.Validations;

namespace ExcelPatternTool.Validation
{
    public class ValidationContainer : IValidationContainer
    {
        public virtual string PropName { get; set; }

        public virtual IValidation Validation { get; set; }

    }
}