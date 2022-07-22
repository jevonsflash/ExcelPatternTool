using System.Runtime.Serialization;

namespace Workshop.Core.Patterns
{
    public enum Convention
    {

        [EnumMember(Value = "Lambda表达式")]
        LambdaExpression,
        [EnumMember(Value = "正则表达式")]
        RegularExpression,
    }
}