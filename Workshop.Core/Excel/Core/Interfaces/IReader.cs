using System.Collections.Generic;
using Workshop.Core.Excel.Models.Interfaces;

namespace Workshop.Core.Excel.Core.Interfaces
{
    interface IReader
    {
        IEnumerable<T> ReadRows<T>(int sheetNumber, int rowsToSkip) where T : IExcelEntity;
        IEnumerable<T> ReadRows<T>(IImportOption importOption) where T : IExcelEntity;
    }
}
