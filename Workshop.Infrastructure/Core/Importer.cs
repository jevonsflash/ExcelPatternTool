using System.Collections.Generic;
using System.Text.RegularExpressions;
using Workshop.Infrastructure.Interfaces;
using Workshop.Infrastructure.Models;

namespace Workshop.Infrastructure.Core
{

    public class Importer
    {
        IReader excelReader;

        public bool LoadXls(byte[] data)
        {
            excelReader = new XlsReader(data);
            return true;
        }
        public bool LoadXlsx(byte[] data)
        {
            excelReader = new XlsxReader(data);
            return true;
        }

        public bool LoadXls(string filePath)
        {
            excelReader = new XlsReader(filePath);
            return true;
        }
        public bool LoadXlsx(string filePath)
        {
            excelReader = new XlsxReader(filePath);
            return true;
        }

        public IEnumerable<T> Process<T>(IImportOption importOption)
        {
            IEnumerable<T> result;
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
