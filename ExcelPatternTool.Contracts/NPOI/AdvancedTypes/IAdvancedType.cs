namespace ExcelPatternTool.Contracts.NPOI.AdvancedTypes
{
    public interface IAdvancedType
    {
        object GetValue();
        void SetValue(object value);
    }
}
