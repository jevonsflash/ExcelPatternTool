using System.Collections.Generic;

namespace Workshop.Infrastructure.Interfaces
{
    interface IReader
    {
        IEnumerable<T> ReadRows<T>(int sheetNumber,int rowsToSkip) where T : IExcelEntity;
        IEnumerable<T> ReadRows<T>(IImportOption importOption) where T : IExcelEntity;
    }
}
