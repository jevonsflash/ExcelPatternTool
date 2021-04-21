using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Workshop.Infrastructure.Interfaces;

namespace Workshop.Infrastructure.Core
{
    public class XlsxReader : BaseReader, IReader
    {
        MemoryStream mem;
        private ISheet sheet;
        public XlsxReader(byte[] data)
        {
            mem = new MemoryStream(data);
            var document = new XSSFWorkbook(mem);
            base.Document = document;
        }

        public XlsxReader(string filePath)
        {
            mem = null;
            var document = new XSSFWorkbook(filePath);
            base.Document = document;
        }

        public IEnumerable<T> ReadRows<T>(IImportOption importOption)
        {

            List<T> result = new List<T>();
            var columns = GetTypeDefinition(typeof(T));
            sheet = Document.GetSheet(importOption.SheetName);
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

        public IEnumerable<T> ReadRows<T>(int sheetNumber, int rowsToSkip)
        {
            List<T> result = new List<T>();
            var columns = GetTypeDefinition(typeof(T));
            sheet = Document.GetSheetAt(sheetNumber);
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;
            for (int i = firstRow + rowsToSkip; i <= lastRow; i++)
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
