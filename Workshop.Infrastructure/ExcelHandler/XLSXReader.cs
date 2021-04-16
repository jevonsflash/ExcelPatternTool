using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Workshop.Infrastructure.ExcelHandler
{
    public class XLSXReader : BaseExcelReader, IExcelReader
    {
        MemoryStream mem;
        private XSSFWorkbook document;
        private ISheet sheet;
        public XLSXReader(byte[] data)
        {
            mem = new MemoryStream(data);
            document = new XSSFWorkbook(mem);
        }

        public XLSXReader(string filePath)
        {
            mem = null;
            document = new XSSFWorkbook(filePath);
        }

        public List<T> ReadRows<T>(IImportOption importOption)
        {

            List<T> result = new List<T>();
            var columns = GetTypeDefinition(typeof(T));
            sheet = document.GetSheet(importOption.SheetName);
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = sheet.FirstRowNum + importOption.SkipRows; i <= lastRow; i++)
            {
                T objectInstance;
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    objectInstance = GetDataToObject<T>(row, columns);
                    result.Add(objectInstance);
                }

            }
            return result;

        }

        public List<T> ReadRows<T>(int sheetNumber, int rowsToSkip)
        {
            List<T> result = new List<T>();
            var columns = GetTypeDefinition(typeof(T));
            sheet = document.GetSheetAt(sheetNumber);
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = sheet.FirstRowNum + rowsToSkip; i <= lastRow; i++)
            {
                T objectInstance;
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    objectInstance = GetDataToObject<T>(row, columns);
                    result.Add(objectInstance);
                }

            }
            return result;
        }

    }
}
