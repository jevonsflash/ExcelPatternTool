using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Workshop.Infrastructure.ExcelHandler
{
    public class ImportOption : IImportOption
    {
        public ImportOption(int sheetNumber, int skipRows)
        {
            SheetNumber = sheetNumber;
            SkipRows = skipRows;

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SheetNumber { get; private set; }
        public int SkipRows { get; private set; }
    }

    public class ImportOption<T> : IImportOption
    {
        public ImportOption(int sheetNumber, int skipRows)
        {
            SheetNumber = sheetNumber;
            SkipRows = skipRows;
            EntityType = typeof(T);

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SheetNumber { get; private set; }
        public int SkipRows { get; private set; }
    }

    public class ImportFromExcel
    {
        private int lastRowNumber = 0;
        public static Regex regexFunctionColor = new Regex(
        "(?:{)(.*)(?:})",
        RegexOptions.IgnoreCase
        | RegexOptions.CultureInvariant
        | RegexOptions.IgnorePatternWhitespace
        | RegexOptions.Compiled);

        private Column initColumn;

        IExcelReader excelReader;

        public ImportFromExcel()
        {

        }
        public bool LoadXls(byte[] data)
        {
            excelReader = new XLSReader(data);
            return true;
        }
        public bool LoadXlsx(byte[] data)
        {
            excelReader = new XLSXReader(data);
            return true;
        }

        public bool LoadXls(string filePath)
        {
            excelReader = new XLSReader(filePath);
            return true;
        }
        public bool LoadXlsx(string filePath)
        {
            excelReader = new XLSXReader(filePath);
            return true;
        }
        /// <summary>
        /// Read a excel sheet from the loaded file
        /// </summary>
        /// <typeparam name="T">Type to fill with row data</typeparam>
        /// <param name="sheetNumber">Index of sheet in the workbook (0-based)</param>
        /// <param name="skipRows">Number of rows to skip at start</param>
        /// <returns>A list of the output type</returns>
        public List<T> ExcelToList<T>(ImportOption importOption)
        {
            List<T> result = new List<T>();
            if (string.IsNullOrEmpty(importOption.SheetName))
            {
                result = excelReader.ReadRows<T>(importOption.SheetNumber, importOption.SkipRows);

            }
            else
            {
                result = excelReader.ReadRows<T>(importOption);
            }
            return result;

        }

        public List<T> ExcelToList<T>(ImportOption<T> importOption)
        {
            List<T> result = new List<T>();
            if (string.IsNullOrEmpty(importOption.SheetName))
            {
                result = excelReader.ReadRows<T>(importOption.SheetNumber, importOption.SkipRows);

            }
            else
            {
                result = excelReader.ReadRows<T>(importOption);
            }
            return result;
        }

    }
}
