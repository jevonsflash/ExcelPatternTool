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
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Common.Helper;
using ExcelPatternTool.Core.StyleMapping;

namespace ExcelPatternTool.Tests.NPOI
{
    [TestClass()]
    public class XlsxWriterTests
    {
        private static readonly string importPath = Path.Combine(CommonHelper.AppBasePath, "case");
        private static readonly string outputPath = Path.Combine(CommonHelper.AppBasePath, "output");

        [TestMethod()]
        public void WriteRowsTest()
        {
            Exporter exporter = new Exporter();

            var filePath = Path.Combine(outputPath, "writeRowsTest.xlsx");
            exporter.DumpXlsx(filePath);

            var eo = new ExportOption<WriteRowTestEntity>(1, 0);
            eo.GenHeaderRow = true;
            var data = GetDatas<WriteRowTestEntity>(Path.Combine(importPath, "test.xlsx"), "常规");
            var bytes = exporter.ProcessGetBytes(data, eo);



            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {

                file.Write(bytes);
            }

        }


        [TestMethod()]
        public void WriteAdvancedTypesTest()
        {
            Exporter exporter = new Exporter();

            var filePath = Path.Combine(outputPath, "writeAdvancedTypesTest.xlsx");
            exporter.DumpXlsx(filePath);

            var eo = new ExportOption<AdvancedTypeTestEntity>(1, 0);
            eo.GenHeaderRow = true;
            var data = GetDatas<AdvancedTypeTestEntity>(Path.Combine(importPath, "test.xlsx"), "高级类型");
            var bytes = exporter.ProcessGetBytes(data, eo);



            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {

                file.Write(bytes);
            }

        }

        [TestMethod()]
        public void WriteCustomStylesTest()
        {
            Exporter exporter = new Exporter();

            var filePath = Path.Combine(outputPath, "writeCustomStylesTest.xlsx");
            exporter.DumpXlsx(filePath);

            var eo = new ExportOption<CustomStyleTestEntity>(1, 0);
            eo.GenHeaderRow = true;
            var data = GetDatas<CustomStyleTestEntity>(Path.Combine(importPath, "test.xlsx"), "自定义样式");
            var bytes = exporter.ProcessGetBytes(data, eo);



            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {

                file.Write(bytes);
            }

        }







        [TestMethod()]
        public void WriteRowsStyleMappingTest()
        {
            Exporter exporter = new Exporter();

            var filePath = Path.Combine(outputPath, "styleMappingTest.xlsx");
            exporter.DumpXlsx(filePath);

            var eo = new ExportOption<EmployeeHealthEntity>(1, 0);
            eo.SheetName = "Sheet1";
            eo.GenHeaderRow = true;
            eo.StyleMapperProvider = typeof(EmployeeHealthEntityStyleMapperProvider);

            var data = GetDatas<EmployeeHealthEntity>(Path.Combine(importPath, "test.xlsx"), "样式映射");
            var bytes = exporter.ProcessGetBytes(data, eo);

            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {

                file.Write(bytes);
            }

        }




        private IEnumerable<T> GetDatas<T>(string filePath = @"D:\test.xlsx", string sheetName = "Sheet1") where T : IExcelEntity
        {
            Importer import = new Importer();
            var data1 = new byte[0];

            data1 = File.ReadAllBytes(filePath);
            import.LoadXlsx(data1);
            var importOption = new ImportOption<T>(0, 1);
            importOption.SheetName = sheetName;
            var output = import.Process<T>(importOption).ToList();

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
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= 5000);
            Assert.IsNotNull(output);

        }
    }
}