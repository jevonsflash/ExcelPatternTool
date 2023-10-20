using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ExcelPatternTool.Contracts.NPOI
{
    public interface IWriter
    {
        Stream WriteRows<T>(IEnumerable<T> dataCollection, string SheetName, int rowsToSkip, bool genHeader);
        Stream WriteRows(Type entityType, IEnumerable dataCollection, string SheetName, int rowsToSkip, bool genHeader);
    }
}