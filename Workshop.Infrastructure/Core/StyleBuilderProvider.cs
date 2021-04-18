using System;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Workshop.Infrastructure.Core
{
    public class StyleBuilderProvider
    {
        public static IStyleBuilder GetStyleBuilder(IWorkbook workbook)
        {
            if (workbook is HSSFWorkbook)
            {
                return new XlsStyleBuilder(workbook);
            }
            else if (workbook is XSSFWorkbook)
            {
                return new XlsxStyleBuilder(workbook);
            }

            return default;

        }
    }
}
