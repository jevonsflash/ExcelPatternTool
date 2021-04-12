using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleExcelImport
{
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
        public List<T> ExcelToList<T>(int sheetNumber,int skipRows)
        {
            List<T> result = new List<T>();
            result=excelReader.ReadRows<T>(sheetNumber, skipRows);
            return result;
        }



    }
}
