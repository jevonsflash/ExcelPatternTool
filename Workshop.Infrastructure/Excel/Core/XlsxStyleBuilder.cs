using System.Drawing;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.UserModel.Extensions;
using Workshop.Infrastructure.Services;

namespace Workshop.Infrastructure.Core
{
    internal class XlsxStyleBuilder : IStyleBuilder
    {
        private IWorkbook Workbook;

        public XlsxStyleBuilder(IWorkbook workbook)
        {
            Workbook = workbook;
        }

        public short GetBuiltIndDataFormat(string dataFormat)
        {
            var result = new XSSFDataFormat(new StylesTable()).GetFormat(dataFormat);
            return result;
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
            comment12.Author = AppConfigurtaionService.Configuration["CellComment:DefaultAuthor"];
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
    }
}
