using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ExcelPatternTool.Core.StyleMapping;

namespace ExcelPatternTool.Core.NPOI
{
    public interface IWriter
    {
        Stream WriteRows<T>(IEnumerable<T> dataCollection, string SheetName, int rowsToSkip, bool genHeader, StyleMapper styleMapper = null);
        Stream WriteRows(Type entityType, IEnumerable dataCollection, string SheetName, int rowsToSkip, bool genHeader, StyleMapper styleMapper = null);
    }
}