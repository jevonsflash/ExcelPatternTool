namespace ExcelPatternTool.Core.Excel.Core.AdvancedTypes
{
    public interface IAdvancedType
    {
        object GetValue();
        void SetValue(object value);
    }
}
