﻿using System.Runtime.Serialization;

namespace ExcelPatternTool.Contracts.Patterns
{
    public enum TableKeyType
    {
        [EnumMember(Value = "无")]
        NoKey,
        [EnumMember(Value = "int")]
        @int,
        [EnumMember(Value = "long")]
        @long,
        [EnumMember(Value = "Guid")]
        Guid,
    }
}