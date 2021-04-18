using System.Drawing;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace Workshop.Infrastructure.Core
{
    internal class XlsStyleBuilder : IStyleBuilder
    {

        private IWorkbook Workbook;
        private short? _palleteColorSize = null;


        public XlsStyleBuilder(IWorkbook workbook)
        {

            Workbook = workbook;
        }

        public short GetBuiltIndDataFormat(string dataFormat)
        {
            var result = HSSFDataFormat.GetBuiltinFormat(dataFormat);
            return result;
        }

        public ICellStyle GetCellStyle(IColor backColor, IColor borderColor, IFont font)
        {
            var cell = Workbook.CreateCellStyle();
            ((HSSFCellStyle)cell).FillForegroundColor = ((HSSFColor)backColor).Indexed;
            ((HSSFCellStyle)cell).LeftBorderColor = ((HSSFColor)borderColor).Indexed;
            ((HSSFCellStyle)cell).RightBorderColor = ((HSSFColor)borderColor).Indexed;
            ((HSSFCellStyle)cell).TopBorderColor = ((HSSFColor)borderColor).Indexed;
            ((HSSFCellStyle)cell).BottomBorderColor = ((HSSFColor)borderColor).Indexed;
            cell.Alignment = HorizontalAlignment.Center;
            cell.BorderBottom = BorderStyle.Thin;
            cell.BorderLeft = BorderStyle.Thin;
            cell.BorderRight = BorderStyle.Thin;
            cell.BorderTop = BorderStyle.Thin;
            cell.FillPattern = FillPattern.SolidForeground;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.SetFont(font);
            return cell;
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

        public IColor GetColor(string htmlColor)
        {
            Color color = ColorTranslator.FromHtml(htmlColor);
            byte[] array = new byte[]
            {
                color.R,
                color.G,
                color.B
            };
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

    }

}
