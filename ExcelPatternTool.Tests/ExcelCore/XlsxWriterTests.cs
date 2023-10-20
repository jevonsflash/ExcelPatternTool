using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelPatternTool.Tests.Entites;
using ExcelPatternTool.Core.NPOI;
using ExcelPatternTool.Contracts.Models;

namespace ExcelPatternTool.Tests.ExcelCore
{
    [TestClass()]
    public class XlsxWriterTests
    {
        [TestMethod()]
        public void WriteRowsTest()
        {
            Exporter exporter = new Exporter();

            var filePath = @"D:\test2.xlsx";
            exporter.DumpXlsx(filePath);

            var eo = new ExportOption<EmployeeEntity>(1, 1);
            eo.SheetName = "Sheet1";
            eo.GenHeaderRow = true;

            var data = GetDatas();
            var bytes = exporter.ProcessGetBytes(data, eo);

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



        [TestMethod()]
        public void LargeDatasTest()
        {

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            var import = new Importer();
            var filePath = @"case\国家药品供应保障综合管理信息平台药品目录YPID(V190715).xlsx";

            import.LoadXlsx(filePath);
            var importOption = new ImportOption<MedicalLibEntity>(0, 1);
            var output = import.Process<MedicalLibEntity>(importOption).ToList();

            stopwatch.Stop();
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= 100000);
            Assert.IsNotNull(output);

        }
    }
}