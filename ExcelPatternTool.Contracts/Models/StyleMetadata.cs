using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NPOI.SS.UserModel;

namespace ExcelPatternTool.Contracts.Models
{
    public class StyleMetadata
    {
        public string FontColor { get; set; } = "#000000";
        public string FontName { get; set; }
        public short FontSize { get; set; }
        public string BorderColor { get; set; } = "#000000";
        public string BackColor { get; set; } = "#FFFFFF";

        public bool IsItalic { get; set; }

        public bool IsBold { get; set; }
        public bool IsStrikeout { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FontSuperScript TypeOffset { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FontUnderlineType Underline { get; set; }


        public StyleMetadata()
        {

        }

        public StyleMetadata(string fontColor, string fontName, short fontSize, string borderColor, string backColor)
        {

            FontColor = fontColor;
            FontName = fontName;
            FontSize = fontSize;
            BorderColor = borderColor;
            BackColor = backColor;

        }
    }

}
