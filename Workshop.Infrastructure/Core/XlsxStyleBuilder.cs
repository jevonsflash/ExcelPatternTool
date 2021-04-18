using System.Drawing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Workshop.Infrastructure.Core
{
    internal class XlsxStyleBuilder : IStyleBuilder
    {
        private IWorkbook Workbook;

        public XlsxStyleBuilder(IWorkbook workbook)
        {
            Workbook = workbook;
        }

        public ICellStyle GetCellStyle(IColor backColor, IColor borderColor, IFont font)
        {
            var cell = Workbook.CreateCellStyle();
            ((XSSFCellStyle)cell).SetFillForegroundColor((XSSFColor)backColor);
            ((XSSFCellStyle)cell).SetLeftBorderColor((XSSFColor)borderColor);
            ((XSSFCellStyle)cell).SetRightBorderColor((XSSFColor)borderColor);
            ((XSSFCellStyle)cell).SetTopBorderColor((XSSFColor)borderColor);
            ((XSSFCellStyle)cell).SetBottomBorderColor((XSSFColor)borderColor);
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
            ((XSSFFont)font).SetColor((XSSFColor)fontColor);
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
            result = new XSSFColor(color);
            return result;
        }


    }
}
