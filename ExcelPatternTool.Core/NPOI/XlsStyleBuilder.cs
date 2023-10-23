using System;
using System.Collections.Generic;

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using ExcelPatternTool.Core.Helper;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ExcelPatternTool.Contracts.NPOI;
using System.Runtime.CompilerServices;
using NPOI.XSSF.UserModel;

namespace ExcelPatternTool.Core.NPOI
{
    public class XlsStyleBuilder : IStyleBuilder, IDisposable
    {

        private IWorkbook Workbook;
        private short? _palleteColorSize = null;


        public XlsStyleBuilder(IWorkbook workbook)
        {

            Workbook = workbook;
        }

        public void SetWorkbook(IWorkbook workbook)
        {
            if (Workbook != workbook)
            {
                Workbook = workbook;

            }
        }

        private List<ICellStyle> _cellStyles = new List<ICellStyle>();

        public ICellStyle GetStyle(IColor backColor)
        {
            ICellStyle result = null;
            foreach (var toStyle in _cellStyles)
            {
                if ((toStyle.FillForegroundColorColor as HSSFColor).GetHexString()
                    == (backColor as HSSFColor).GetHexString())
                {
                    result = toStyle;
                }
            }
            if (result == null)
            {
                var newStyle = Workbook.CreateCellStyle();
                newStyle.Alignment = HorizontalAlignment.Center;
                newStyle.BorderBottom = BorderStyle.Thin;
                newStyle.BorderLeft = BorderStyle.Thin;
                newStyle.BorderRight = BorderStyle.Thin;
                newStyle.BorderTop = BorderStyle.Thin;
                newStyle.FillPattern = FillPattern.SolidForeground;
                newStyle.VerticalAlignment = VerticalAlignment.Center;
                ((HSSFCellStyle)newStyle).FillForegroundColor = ((HSSFColor)backColor).Indexed;
                _cellStyles.Add(newStyle);
                result = newStyle;
            }
            return result;

        }



        public short GetBuiltIndDataFormat(string dataFormat)
        {
            var result = HSSFDataFormat.GetBuiltinFormat(dataFormat);
            return result;
        }

        public ICellStyle GetCellStyle(IColor backColor, IColor borderColor, IFont font)
        {
            var cell = GetStyle(backColor);
            if (borderColor != null)
            {
                ((HSSFCellStyle)cell).LeftBorderColor = ((HSSFColor)borderColor).Indexed;
                ((HSSFCellStyle)cell).RightBorderColor = ((HSSFColor)borderColor).Indexed;
                ((HSSFCellStyle)cell).TopBorderColor = ((HSSFColor)borderColor).Indexed;
                ((HSSFCellStyle)cell).BottomBorderColor = ((HSSFColor)borderColor).Indexed;
            }
            if (font != null)
            {
                cell.SetFont(font);
            }
            return cell;
        }
        public IColor GetFontColor(ICellStyle cellStyle)
        {
            var result = (cellStyle.GetFont(Workbook) as HSSFFont).GetHSSFColor(Workbook as HSSFWorkbook);
            return result;
        }

        public IFont GetFont(short fontSize, string fontName, IColor fontColor, bool isItalic, bool isBold, bool isStrikeout, FontSuperScript typeOffset, FontUnderlineType underline)
        {
            var font = Workbook.CreateFont();
            font.IsBold = isBold;
            font.IsItalic = isItalic;
            font.IsStrikeout = isStrikeout;
            font.TypeOffset = typeOffset;
            font.Underline = underline;
            var index = ((HSSFColor)fontColor).Indexed;
            ((HSSFFont)font).Color = index;
            font.FontName = fontName;
            font.FontHeightInPoints = fontSize;
            return font;
        }

        public IColor GetBackgroundColor(ICellStyle cellStyle)
        {
            var result = (cellStyle as HSSFCellStyle).FillForegroundColorColor;
            return result;

        }
        public IColor GetBoarderColor(ICellStyle cellStyle)
        {
            //can't be realized 
            //var result = (cellStyle as HSSFCellStyle).BottomBorderColor;
            var result = new HSSFColor.Automatic();
            return result;

        }

        public IColor GetColor(string htmlColor)
        {
            if (string.IsNullOrEmpty(htmlColor))
            {
                return new HSSFColor.Automatic();
            }

            var color = Rgba32.ParseHex(htmlColor);


            IColor result;
            HSSFPalette customPalette = (Workbook as HSSFWorkbook).GetCustomPalette();
            if (_palleteColorSize >= 63)
            {
                HSSFColor hSSFColor = customPalette.FindColor(color.R, color.G, color.B);
                if (hSSFColor == null)
                {
                    hSSFColor = customPalette.FindSimilarColor(color.R, color.G, color.B);
                }
                short? palleteColorSize = _palleteColorSize;
                _palleteColorSize = palleteColorSize.HasValue
                    ? new short?((short)(palleteColorSize.GetValueOrDefault() + 1))
                    : null;
                result = hSSFColor;
            }
            else
            {
                if (!_palleteColorSize.HasValue)
                {
                    _palleteColorSize = new short?(8);
                }
                else
                {
                    short? palleteColorSize = _palleteColorSize;
                    _palleteColorSize = palleteColorSize.HasValue
                        ? new short?((short)(palleteColorSize.GetValueOrDefault() + 1))
                        : null;
                }
                customPalette.SetColorAtIndex(_palleteColorSize.Value, color.R, color.G, color.B);
                HSSFColor hSSFColor = customPalette.GetColor(_palleteColorSize.Value);
                result = hSSFColor;
            }
            return result;
        }
        public IRichTextString GetCommentInfo(string comment)
        {
            var result = new HSSFRichTextString("批注: " + comment);
            return result;
        }

        public IComment GetComment(IRichTextString richTextString)
        {
            //https://www.cnblogs.com/zhuangjolon/p/9300704.html
            HSSFPatriarch patr = (HSSFPatriarch)Workbook.GetSheetAt(0).CreateDrawingPatriarch();
            HSSFComment comment12 = patr.CreateComment(new HSSFClientAnchor(0, 0, 0, 0, 1, 2, 2, 3));//批注显示定位        }
            comment12.String = richTextString;
            comment12.Author = ConfigurationHelper.GetConfigValue("CellComment:DefaultAuthor", "Linxiao");
            return comment12;

        }

        public IComment GetComment(string comment)
        {
            var text = new HSSFRichTextString("批注: " + comment);
            return GetComment(text);

        }
        public string GetARGBFromIColor(IColor fontColor)
        {
            if (fontColor != null)
            {
                var argb = (fontColor as HSSFColor).GetHexString();
                if (string.IsNullOrEmpty(argb))
                {
                    return null;
                }
                var result = string.Format("#{0}", argb.Substring(2));
                return result;
            }
            return null;
        }

        public void Dispose()
        {
            Workbook.Close();
            Workbook = null;
        }
    }
}
