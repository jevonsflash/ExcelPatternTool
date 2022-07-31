using System.Runtime.Serialization;

namespace ExcelPatternTool.Core.Patterns
{
    public enum Convention
    {

        [EnumMember(Value = "普通校验器")]
        LambdaExpression,
        [EnumMember(Value = "正则表达式校验器")]
        RegularExpression,
        //[EnumMember(Value = "自定义校验器")]
        //MyExpression,
    }
}