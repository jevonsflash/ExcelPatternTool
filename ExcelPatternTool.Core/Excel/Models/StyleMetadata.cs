using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExcelPatternTool.Core.Excel.Attributes;

namespace ExcelPatternTool.Core.Excel.Models
{
    public class StyleMetadata 
    {
        public string FontColor { get; set; }
        public string FontName { get; set; }
        public short FontSize { get; set; }
        public string BorderColor { get; set; }
        public string BackColor { get; set; }



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
