using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Workshop.Infrastructure.Models;

namespace Workshop.Infrastructure.Core
{
    public class BaseHandler
    {

        public IWorkbook Document { get; set; }

        public BaseHandler()
        {
        }
        internal ICellStyle MetaToCellStyle(StyleMetadata rowStyle)
        {
            var borderColor = StyleBuilderProvider.GetStyleBuilder(this.Document).GetColor(rowStyle.BorderColor);
            var backColor = StyleBuilderProvider.GetStyleBuilder(this.Document).GetColor(rowStyle.BackColor);
            var font = this.CreateFont(rowStyle);
            ICellStyle cellStyle;
            var styleBuilder = StyleBuilderProvider.GetStyleBuilder(this.Document);
            cellStyle = styleBuilder.GetCellStyle(backColor, borderColor, font);

            return cellStyle;
        }

        internal IFont CreateFont(StyleMetadata rowStyle)
        {
            var fontColor = StyleBuilderProvider.GetStyleBuilder(this.Document).GetColor(rowStyle.FontColor);
            return StyleBuilderProvider.GetStyleBuilder(this.Document).GetFont(rowStyle.FontSize, rowStyle.FontName, fontColor);
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
            return cellStyleMeta;
        }

        
    }
}