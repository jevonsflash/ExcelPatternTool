using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Infrastructure.Models
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
