using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.UserModel.Extensions;
using ExcelPatternTool.Core.Excel.Core.Interfaces;
using ExcelPatternTool.Core.Excel.Services;
using ExcelPatternTool.Core.Helper;

namespace ExcelPatternTool.Core.Excel.Core
{
    public class XlsxStyleBuilder : IStyleBuilder, IDisposable
    {
        private IWorkbook Workbook;

        public XlsxStyleBuilder(IWorkbook workbook)
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
                if ((toStyle.FillForegroundColorColor as XSSFColor).ARGBHex
                    == (backColor as XSSFColor).ARGBHex)

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
                ((XSSFCellStyle)newStyle).SetFillForegroundColor((XSSFColor)backColor);
                _cellStyles.Add(newStyle);
                result = newStyle;
            }
            return result;

        }

        public short GetBuiltIndDataFormat(string dataFormat)
        {
            var result = new XSSFDataFormat(new StylesTable()).GetFormat(dataFormat);
            return result;
        }
        public ICellStyle GetCellStyle(IColor backColor, IColor borderColor, IFont font)
        {
            var cell = GetStyle(backColor);
            if (borderColor != null)
            {
                ((XSSFCellStyle)cell).SetLeftBorderColor((XSSFColor)borderColor);
                ((XSSFCellStyle)cell).SetRightBorderColor((XSSFColor)borderColor);
                ((XSSFCellStyle)cell).SetTopBorderColor((XSSFColor)borderColor);
                ((XSSFCellStyle)cell).SetBottomBorderColor((XSSFColor)borderColor);
            }


            if (font != null)
            {
                cell.SetFont(font);

            }
            return cell;
        }

        public IColor GetFontColor(ICellStyle cellStyle)
        {
            var result = (cellStyle.GetFont(Workbook) as XSSFFont).GetXSSFColor();
            return result;
        }

        public IFont GetFont(short fontSize, string fontName, IColor fontColor)
        {
            var font = Workbook.CreateFont();
            font.Boldweight = 100;
            ((XSSFFont)font).SetColor((XSSFColor)fontColor);
            font.FontName = fontName;
            font.FontHeightInPoints = fontSize;
            return font;
        }
        public IColor GetColor(string htmlColor)
        {
            if (string.IsNullOrEmpty(htmlColor))
            {
                return new XSSFColor(IndexedColors.Automatic);
            }
            var result = new XSSFColor(Color.ParseHex(htmlColor));
            return result;
        }

        public IRichTextString GetCommentInfo(string comment)
        {
            var result = new XSSFRichTextString("批注: " + comment);
            return result;
        }
        public IComment GetComment(IRichTextString richTextString)
        {
            IDrawing patr = Workbook.GetSheetAt(0).CreateDrawingPatriarch();
            IComment comment12 = patr.CreateCellComment(new XSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0));//批注显示定位        }
            comment12.String = richTextString;
            comment12.Author = ConfigurationHelper.GetConfigValue("CellComment:DefaultAuthor", "Linxiao");
            return comment12;

        }

        public IComment GetComment(string comment)
        {
            var text = new XSSFRichTextString("批注: " + comment);
            return GetComment(text);

        }

        public IColor GetBackgroundColor(ICellStyle cellStyle)
        {
            var result = (cellStyle as XSSFCellStyle).FillForegroundColorColor;
            return result;

        }

        public IColor GetBoarderColor(ICellStyle cellStyle)
        {
            var result = (cellStyle as XSSFCellStyle).GetBorderColor(BorderSide.BOTTOM);
            return result;

        }

        public string GetARGBFromIColor(IColor fontColor)
        {
            if (fontColor != null)
            {
                var argb = (fontColor as XSSFColor).ARGBHex;
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
