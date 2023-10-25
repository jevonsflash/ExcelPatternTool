using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using ExcelPatternTool.Core.EntityProxy;
using ExcelPatternTool.Tests.Entites;
using ExcelPatternTool.Core.NPOI;
using ExcelPatternTool.Contracts.Models;

namespace ExcelPatternTool.Tests.NPOI
{
    [TestClass()]
    public class XlsxReaderTests
    {
        [TestMethod()]
        public void ReadRowsTest()
        {
            Importer import = new Importer();
            var filePath = @"D:\test.xlsx";
            var data1 = new byte[0];

            data1 = File.ReadAllBytes(filePath);
            import.LoadXlsx(data1);
            var importOption = new ImportOption<WriteRowTestEntity>(0, 2);
            importOption.SheetName = "全职";
            var output = import.Process<WriteRowTestEntity>(importOption).ToList();
            Assert.IsNotNull(output);
        }
    }
}