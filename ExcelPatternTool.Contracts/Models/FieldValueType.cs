using System.Runtime.Serialization;

namespace ExcelPatternTool.Contracts.Models
{
    public enum FieldValueType
    {
        [EnumMember(Value = "自定义")]
        Any = 0,
        [EnumMember(Value = "文本")]
        Text = 1,
        [EnumMember(Value = "数值")]
        Numeric = 2,
        [EnumMember(Value = "时间")]
        Date = 3,
        [EnumMember(Value = "布尔值")]
        Bool = 4
    }
}
