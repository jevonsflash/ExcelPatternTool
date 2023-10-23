using System;
using System.Collections.Generic;
using ExcelPatternTool.Contracts;

namespace ExcelPatternTool.Core.NPOI
{
    public interface IReader
    {
        IEnumerable<T> ReadRows<T>(int sheetNumber, int rowsToSkip) where T : IExcelEntity;
        IEnumerable<T> ReadRows<T>(IImportOption importOption) where T : IExcelEntity;
        IEnumerable<IExcelEntity> ReadRows(Type entityType, IImportOption importOption);
        IEnumerable<IExcelEntity> ReadRows(Type entityType, int sheetNumber, int rowsToSkip);
    }
}
