﻿using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.UserModel.Extensions;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Contracts.NPOI;
using NPOI.HSSF.Util;
using NPOI.OOXML.XSSF.UserModel;

namespace ExcelPatternTool.Core.NPOI
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

        private bool IsSameFont(IFont a, IFont b)
        {
            return
           a.Color != 0 && a.Color == b.Color
            && a.FontName == b.FontName
            && a.FontHeightInPoints == b.FontHeightInPoints
            && a.IsItalic == b.IsItalic
            && a.IsBold == b.IsBold
            && a.IsStrikeout == b.IsStrikeout
            ;
        }


        public short GetBuiltIndDataFormat(string dataFormat)
        {
            var result = (short)-1;
            return result;
        }
        public ICellStyle GetCellStyle(IColor backColor, IColor borderColor, IFont font)
        {
            ICellStyle cell = null;
            foreach (var toStyle in _cellStyles)
            {
                var currentFont = toStyle.GetFont(Workbook);
                if (currentFont != null && (toStyle.FillForegroundColorColor as XSSFColor).ARGBHex
                    == (backColor as XSSFColor).ARGBHex &&
                   IsSameFont(currentFont, font)
                    )

                {
                    cell = toStyle;
                }
            }
            if (cell == null)
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
                cell = newStyle;
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
            }
            _cellStyles.Add(cell);

            return cell;
        }

        public IColor GetFontColor(ICellStyle cellStyle)
        {
            var result = (cellStyle.GetFont(Workbook) as XSSFFont).GetXSSFColor();
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
            var index = ((XSSFColor)fontColor).Index;
            if (index != 0)
            {
                ((XSSFFont)font).Color = index;
            }
            else
            {
                ((XSSFFont)font).SetColor((XSSFColor)fontColor);
            }

            font.FontName = fontName;
            font.FontHeightInPoints = fontSize;
            return font;
        }
        public IColor GetColor(string htmlColor)
        {
            if (string.IsNullOrEmpty(htmlColor))
            {
                return new XSSFColor(IndexedColors.Automatic, new DefaultIndexedColorMap());
            }
            var result = new XSSFColor(Color.Parse(htmlColor));
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
