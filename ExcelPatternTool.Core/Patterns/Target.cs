using System.Runtime.Serialization;

namespace ExcelPatternTool.Core.Patterns
{
    public enum Target
    {

        [EnumMember(Value = "单元格公式")]
        Formula,
        [EnumMember(Value = "单元格数值")]
        Value,
    }
}