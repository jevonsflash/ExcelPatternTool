using System.Runtime.Serialization;

namespace ExcelPatternTool.Core.Patterns
{
    public enum CellType
    {
        [EnumMember(Value = "常规")]
        GeneralType,
        [EnumMember(Value = "包含注解")]
        CommentedType,
        [EnumMember(Value = "包含样式")]
        StyledType,
        [EnumMember(Value = "包含公式")]
        FormulatedType,
        [EnumMember(Value = "全包含")]
        FullAdvancedType,
    }
}