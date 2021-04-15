using System.Collections.Generic;

namespace Workshop.Infrastructure.ExcelHandler
{
    interface IExcelReader
    {
        List<T> ReadRows<T>(int sheetNumber,int rowsToSkip);
        List<T> ReadRows<T>(IImportOption importOption);
    }
}
