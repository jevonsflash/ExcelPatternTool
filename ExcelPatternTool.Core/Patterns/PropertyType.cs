using System.Runtime.Serialization;

namespace ExcelPatternTool.Core.Patterns
{
    public enum PropertyType
    {
        [EnumMember(Value = "字符串")]
        @string,
        [EnumMember(Value = "日期")]
        DateTime,
        [EnumMember(Value = "整型")]
        @int,
        [EnumMember(Value = "浮点型")]
        @double,
        [EnumMember(Value = "布尔")]
        @bool,
    }
}