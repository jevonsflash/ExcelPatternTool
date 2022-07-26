using System.Runtime.Serialization;

namespace Workshop.Core.Patterns
{
    public enum CellType
    {
        [EnumMember(Value = "常规类型")]
        GeneralType,
        [EnumMember(Value = "包含注解类型")]
        CommentedType,
        [EnumMember(Value = "包含样式类型")]
        StyledType,
        [EnumMember(Value = "包含公式类型")]
        FormulatedType,
        [EnumMember(Value = "包含注解，样式，公式类型")]
        FullAdvancedType,
    }
}