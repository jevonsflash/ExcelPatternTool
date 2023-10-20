using System.Runtime.Serialization;

namespace ExcelPatternTool.Contracts.Patterns
{
    public enum Target
    {

        [EnumMember(Value = "单元格公式")]
        Formula,
        [EnumMember(Value = "单元格数值")]
        Value,
    }
}