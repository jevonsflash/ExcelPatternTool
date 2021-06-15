using System;
using System.Collections.Generic;
using System.Drawing;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using Workshop.Infrastructure.Services;

namespace Workshop.Infrastructure.Core
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
            if (this.Workbook != workbook)
            {
                this.Workbook = workbook;

            }
        }

        private List<ICellStyle> _cellStyles = new List<ICellStyle>();

        public ICellStyle GetStyle(IColor backColor)
        {
            ICellStyle result = null;
            foreach (var toStyle in this._cellStyles)
            {
                if ((toStyle.FillForegroundColorColor as HSSFColor).GetHexString()
                    == (backColor  as HSSFColor).GetHexString())
                {
                    result = toStyle;
                }
            }
            if (result == null)
            {
                var newStyle = this.Workbook.CreateCellStyle();
                newStyle.Alignment = HorizontalAlignment.Center;
                newStyle.BorderBottom = BorderStyle.Thin;
                newStyle.BorderLeft = BorderStyle.Thin;
                newStyle.BorderRight = BorderStyle.Thin;
                newStyle.BorderTop = BorderStyle.Thin;
                newStyle.FillPattern = FillPattern.SolidForeground;
                newStyle.VerticalAlignment = VerticalAlignment.Center;
                ((HSSFCellStyle)newStyle).FillForegroundColor = ((HSSFColor)backColor).Indexed;
                this._cellStyles.Add(newStyle);
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
            var cell = this.GetStyle(backColor);
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

        public IFont GetFont(short fontSize, string fontName, IColor fontColor)
        {
            var font = Workbook.CreateFont();
            font.Boldweight = 100;
            ((HSSFFont)font).Color = ((HSSFColor)fontColor).Indexed;
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
            Color color = ColorTranslator.FromHtml(htmlColor);

            IColor result;
            HSSFPalette customPalette = (this.Workbook as HSSFWorkbook).GetCustomPalette();
            if (this._palleteColorSize >= 63)
            {
                HSSFColor hSSFColor = customPalette.FindColor(color.R, color.G, color.B);
                if (hSSFColor == null)
                {
                    hSSFColor = customPalette.FindSimilarColor(color.R, color.G, color.B);
                }
                short? palleteColorSize = this._palleteColorSize;
                this._palleteColorSize = (palleteColorSize.HasValue
                    ? new short?((short)(palleteColorSize.GetValueOrDefault() + 1))
                    : null);
                result = hSSFColor;
            }
            else
            {
                if (!this._palleteColorSize.HasValue)
                {
                    this._palleteColorSize = new short?(8);
                }
                else
                {
                    short? palleteColorSize = this._palleteColorSize;
                    this._palleteColorSize = (palleteColorSize.HasValue
                        ? new short?((short)(palleteColorSize.GetValueOrDefault() + 1))
                        : null);
                }
                customPalette.SetColorAtIndex(this._palleteColorSize.Value, color.R, color.G, color.B);
                HSSFColor hSSFColor = customPalette.GetColor(this._palleteColorSize.Value);
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
            comment12.Author = AppConfigurtaionService.Configuration["CellComment:DefaultAuthor"];
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
            this.Workbook.Close();
            this.Workbook = null;
        }
    }
}
