using NPOI.SS.UserModel;

namespace Workshop.Infrastructure.Core
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
        IFont GetFont(short fontSize, string fontName, IColor fontColor);
        /// <summary>
        /// 获取颜色
        /// </summary>
        /// <param name="htmlColor"></param>
        /// <returns></returns>
        IColor GetColor(string htmlColor);

    }
}