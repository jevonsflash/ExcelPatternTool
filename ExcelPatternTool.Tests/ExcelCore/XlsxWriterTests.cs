using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExcelPatternTool.Core.Excel.Core;
using ExcelPatternTool.Core.Excel.Models;
using ExcelPatternTool.Tests.Entites;

namespace ExcelPatternTool.Infrastructure.Tests
{
    [TestClass()]
    public class XlsxWriterTests
    {
        [TestMethod()]
        public void WriteRowsTest()
        {
            Exporter exporter=new Exporter();

            var filePath = @"D:\test2.xlsx";
            exporter.DumpXlsx(filePath);
            
            var eo=new ExportOption(1,1);
            eo.SheetName = "Sheet1";
            eo.GenHeaderRow = true;

            var data = GetDatas();
            var bytes = exporter.ProcessGetBytes(data,eo);

            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
              
                file.Write(bytes);
            }

        }

        private IEnumerable<EmployeeEntity> GetDatas()
        {
            Importer import = new Importer();
            var filePath = @"D:\test.xlsx";
            var data1 = new byte[0];

            data1 = File.ReadAllBytes(filePath);
            import.LoadXlsx(data1);
            var importOption = new ImportOption<EmployeeEntity>(0, 2);
            importOption.SheetName = "Sheet2";
            var output = import.Process<EmployeeEntity>(importOption).ToList();

            return output;
        }
    }
}