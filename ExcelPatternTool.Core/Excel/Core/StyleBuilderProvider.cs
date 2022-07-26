using System;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ExcelPatternTool.Core.Excel.Core.Interfaces;

namespace ExcelPatternTool.Core.Excel.Core
{
    public class StyleBuilderProvider
    {
        private static XlsStyleBuilder _xlsStyleBuilder = null;
        private static XlsxStyleBuilder _xlsxStyleBuilder = null;

        public static void DisposeCurrent()
        {
            if (_xlsStyleBuilder != null)
            {
                _xlsStyleBuilder.Dispose();
                _xlsStyleBuilder = null;
            }

            if (_xlsxStyleBuilder != null)
            {
                _xlsxStyleBuilder.Dispose();
                _xlsxStyleBuilder = null;
            }
            GC.Collect();
        }
        public static string GetWorkbookType(IWorkbook workbook)
        {
            if (workbook is HSSFWorkbook)
            {
                return "xls";
            }

            else if (workbook is XSSFWorkbook)
            {
                return "xlsx";


            }
            return default;
        }

        public static IStyleBuilder GetStyleBuilder(IWorkbook workbook)
        {
            IStyleBuilder result;
            if (workbook is HSSFWorkbook)
            {
                if (_xlsStyleBuilder != null)
                {
                    _xlsStyleBuilder.SetWorkbook(workbook);
                }
                else
                {
                    _xlsStyleBuilder = new XlsStyleBuilder(workbook); ;


                }
                result = _xlsStyleBuilder;

            }
            else if (workbook is XSSFWorkbook)
            {
                if (_xlsxStyleBuilder != null)
                {
                    _xlsxStyleBuilder.SetWorkbook(workbook);

                }
                else
                {
                    _xlsxStyleBuilder = new XlsxStyleBuilder(workbook);

                }
                result = _xlsxStyleBuilder;
            }
            else
            {
                result = default;
            }
            return result;

        }
    }
}
