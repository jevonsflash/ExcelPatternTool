using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using NPOI.SS.UserModel;

namespace ExcelPatternTool.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class StyleAttribute : Attribute
    {
        public string FontColor { get; set; }

        public string FontName { get; set; }

        public short FontSize { get; set; }

        public string BorderColor { get; set; }

        public string BackColor { get; set; }

        public bool IsItalic { get; set; }

        public bool IsBold { get; set; }
        public bool IsStrikeout { get; set; }

        public FontSuperScript TypeOffset { get; set; }
        public FontUnderlineType Underline { get; set; }

        public StyleAttribute()
        {

        }
    }
}