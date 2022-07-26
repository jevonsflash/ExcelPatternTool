using System.Collections.Generic;
using ExcelPatternTool.Core.Excel.Models.Interfaces;

namespace ExcelPatternTool.Core.Excel.Core.Interfaces
{
    interface IReader
    {
        IEnumerable<T> ReadRows<T>(int sheetNumber, int rowsToSkip) where T : IExcelEntity;
        IEnumerable<T> ReadRows<T>(IImportOption importOption) where T : IExcelEntity;
    }
}
