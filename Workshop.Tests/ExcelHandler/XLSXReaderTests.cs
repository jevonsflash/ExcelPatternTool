using Microsoft.VisualStudio.TestTools.UnitTesting;
using Workshop.Infrastructure.ExcelHandler;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Workshop.Core.Entites;

namespace Workshop.Infrastructure.ExcelHandler.Tests
{
    [TestClass()]
    public class XLSXReaderTests
    {
        [TestMethod()]
        public void ReadRowsTest()
        {
            ImportFromExcel import = new ImportFromExcel();
            var filePath = @"D:\test.xlsx";
            var data1 = new byte[0];

            data1 = File.ReadAllBytes(filePath);
            import.LoadXlsx(data1);
            var importOption = new ImportOption<EmpoyeeImportEntity>(0, 2);
            importOption.SheetName = "全职";
            var output = import.ExcelToList<EmpoyeeImportEntity>(importOption).ToList();

            Assert.IsNotNull(output);
        }
    }
}