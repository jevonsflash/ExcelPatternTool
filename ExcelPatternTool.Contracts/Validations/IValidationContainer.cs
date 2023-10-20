namespace ExcelPatternTool.Contracts.Validations
{
    public interface IValidationContainer
    {
        public string PropName { get; set; }

        public IValidation Validation { get; set; }

    }
}