using System;

namespace ExcelPatternTool.Core.Excel.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class StyleAttribute : Attribute
    {
        public string FontColor { get; set; }

        public string FontName { get; set; }

        public short FontSize { get; set; }

        public string BorderColor { get; set; }

        public string BackColor { get; set; }

        public StyleAttribute()
        {

        }
    }
}