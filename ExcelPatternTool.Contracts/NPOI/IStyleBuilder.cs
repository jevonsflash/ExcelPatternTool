using NPOI.SS.UserModel;

namespace ExcelPatternTool.Contracts.NPOI
{
    public interface IStyleBuilder
    {

        /// <summary>
        /// 获取CellStyle
        /// </summary>
        /// <param name="backColor"></param>
        /// <param name="borderColor"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        ICellStyle GetCellStyle(IColor backColor, IColor borderColor, IFont font);
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="fontSize"></param>
        /// <param name="fontName"></param>
        /// <param name="fontColor"></param>
        /// <returns></returns>
        IFont GetFont(short fontSize, string fontName, IColor fontColor, bool isItalic, bool isBold, bool isStrikeout, FontSuperScript typeOffset, FontUnderlineType underline);
        /// <summary>
        /// 获取颜色
        /// </summary>
        /// <param name="htmlColor"></param>
        /// <returns></returns>
        IColor GetColor(string htmlColor);
        /// <summary>
        /// 获取内建格式
        /// </summary>
        /// <param name="dataFormat"></param>
        /// <returns></returns>
        short GetBuiltIndDataFormat(string dataFormat);
        IRichTextString GetCommentInfo(string comment);
        IComment GetComment(IRichTextString richTextString);
        IComment GetComment(string comment);
        IColor GetFontColor(ICellStyle cellStyle);
        string GetARGBFromIColor(IColor fontColor);
        IColor GetBackgroundColor(ICellStyle cellStyle);
        IColor GetBoarderColor(ICellStyle cellStyle);

        void SetWorkbook(IWorkbook workbook);
        ICellStyle GetStyle(IColor backColor);
    }
}