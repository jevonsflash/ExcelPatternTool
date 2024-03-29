﻿using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ExcelPatternTool.Contracts.Models;

namespace ExcelPatternTool.Core.NPOI
{
    public class BaseHandler
    {

        public IWorkbook Document { get; set; }

        public BaseHandler()
        {
        }
        internal ICellStyle MetaToCellStyle(StyleMetadata rowStyle)
        {
            var styleBuilder = StyleBuilderProvider.GetStyleBuilder(Document);
            var borderColor = styleBuilder.GetColor(rowStyle.BorderColor);
            var backColor = styleBuilder.GetColor(rowStyle.BackColor);
            var font = CreateFont(rowStyle);

            var cellStyle = styleBuilder.GetCellStyle(backColor, borderColor, font);
            //var cellStyle = styleBuilder.GetCellStyle(backColor, null, null);

            return cellStyle;
        }

        internal IFont CreateFont(StyleMetadata rowStyle)
        {
            var fontColor = StyleBuilderProvider.GetStyleBuilder(Document).GetColor(rowStyle.FontColor);
            return StyleBuilderProvider.GetStyleBuilder(Document).GetFont(rowStyle.FontSize, rowStyle.FontName, fontColor, rowStyle.IsItalic, rowStyle.IsBold, rowStyle.IsStrikeout, rowStyle.TypeOffset, rowStyle.Underline);
        }
        internal StyleMetadata CellStyleToMeta(ICellStyle cellStyle)
        {
            var fontName = cellStyle.GetFont(Document).FontName;
            var fontSize = (short)cellStyle.GetFont(Document).FontHeightInPoints;
            var fontColor = StyleBuilderProvider.GetStyleBuilder(Document).GetFontColor(cellStyle);

            var borderColor = StyleBuilderProvider.GetStyleBuilder(Document).GetBoarderColor(cellStyle);
            var backColor = StyleBuilderProvider.GetStyleBuilder(Document).GetBackgroundColor(cellStyle);
            string argb = StyleBuilderProvider.GetStyleBuilder(Document).GetARGBFromIColor(fontColor);
            string argb2 = StyleBuilderProvider.GetStyleBuilder(Document).GetARGBFromIColor(borderColor);
            string argb3 = StyleBuilderProvider.GetStyleBuilder(Document).GetARGBFromIColor(backColor);
            var cellStyleMeta = new StyleMetadata(argb, fontName, fontSize, argb2, argb3);
            cellStyleMeta.IsBold = cellStyle.GetFont(Document).IsBold;
            cellStyleMeta.IsItalic = cellStyle.GetFont(Document).IsItalic;
            cellStyleMeta.IsStrikeout = cellStyle.GetFont(Document).IsStrikeout;
            cellStyleMeta.TypeOffset = cellStyle.GetFont(Document).TypeOffset;
            cellStyleMeta.Underline = cellStyle.GetFont(Document).Underline;
            return cellStyleMeta;
        }


    }
}