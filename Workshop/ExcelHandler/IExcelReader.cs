using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExcelImport
{
    interface IExcelReader
    {
        List<T> ReadRows<T>(int sheetNumber,int rowsToSkip);
    }
}
