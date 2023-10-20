using System.Runtime.Serialization;

namespace ExcelPatternTool.Contracts.Patterns
{
    public enum PropertyType
    {
        [EnumMember(Value = "string")]
        @string,
        [EnumMember(Value = "DateTime")]
        DateTime,
        [EnumMember(Value = "int")]
        @int,
        [EnumMember(Value = "double")]
        @double,
        [EnumMember(Value = "bool")]
        @bool,
    }
}