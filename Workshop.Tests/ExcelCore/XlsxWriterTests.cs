using Microsoft.VisualStudio.TestTools.UnitTesting;
using Workshop.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Entites;
using Workshop.Infrastructure.Models;

namespace Workshop.Infrastructure.Tests
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
            eo.SheetName = "生成1";
            eo.GenHeaderRow = true;

            var data = GetDatas();
            var bytes = exporter.ProcessGetBytes(data,eo);

            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
              
                file.Write(bytes);
            }

        }

        private IEnumerable<EmpoyeeEntity> GetDatas()
        {
            Importer import = new Importer();
            var filePath = @"D:\test.xlsx";
            var data1 = new byte[0];

            data1 = File.ReadAllBytes(filePath);
            import.LoadXlsx(data1);
            var importOption = new ImportOption<EmpoyeeEntity>(0, 2);
            importOption.SheetName = "全职";
            var output = import.Process<EmpoyeeEntity>(importOption).ToList();

            return output;
        }
    }
}