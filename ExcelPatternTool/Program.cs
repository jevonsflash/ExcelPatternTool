using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Models;
using ExcelPatternTool.Core.EntityProxy;
using ExcelPatternTool.Core.Patterns;
using Newtonsoft.Json;
using System.Diagnostics;
using DirFileHelper = ExcelPatternTool.Common.Helper.DirFileHelper;

namespace ExcelPatternTool
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (!CliProcessor.ProcessCommandLine(args))
            {
                Console.WriteLine("缺少参数或参数不正确");

                CliProcessor.Usage();
                Environment.ExitCode = 1;
                return;
            }
            try
            {

                var serializedstr = DirFileHelper.ReadFile(CliProcessor.patternFilePath);
                var _pattern = JsonConvert.DeserializeObject<Pattern>(serializedstr);

                ValidateProcessor.Init();
                DbProcessor.Init();

                var sw = Stopwatch.StartNew();

                var op1 = new ImportOption(EntityProxyContainer.Current.EntityType, _pattern.ExcelImport.SheetNumber, _pattern.ExcelImport.SkipRow);
                op1.SheetName=_pattern.ExcelImport.SheetName;
                List<IExcelEntity> result;
                if (CliProcessor.source=="excel")
                {
                    result = DocProcessor.ImportFrom(CliProcessor.inputPathList.First(), op1);
                }
                else
                {
                    result = DbProcessor.ImportFromDb(EntityProxyContainer.Current.EntityType, CliProcessor.source, CliProcessor.outputPathList.First());
                }
                ValidateProcessor.GetDataAction(result);

                Console.WriteLine("校验完成，发现{0}个问题", ValidateProcessor.ProcessResultList.Count);
                Console.WriteLine("ID\t位置\t严重程度\t说明");


                foreach (var item in ValidateProcessor.ProcessResultList)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", item.Id, item.Position, item.Level, item.Content);
                }
                if (ValidateProcessor.ProcessResultList.Count>0)
                {
                    Console.WriteLine("校验未通过 ,Time taken: {0}", sw.Elapsed);
                    Environment.ExitCode = 1;
                    return;
                }
                if (CliProcessor.destination=="excel")
                {
                    var op2 = new ExportOption(EntityProxyContainer.Current.EntityType, _pattern.ExcelExport.SheetNumber, _pattern.ExcelExport.SkipRow);
                    op2.SheetName=_pattern.ExcelExport.SheetName;
                    op2.GenHeaderRow=_pattern.ExcelExport.GenHeaderRow;

                    DocProcessor.SaveTo(
                        EntityProxyContainer.Current.EntityType,
                        CliProcessor.outputPathList.First(),
                        result, op2
                        );
                    Console.WriteLine("已成功完成，导出共 {0} 条数据", result.Count);

                }
                else
                {
                    DbProcessor.ExportToDb(result, CliProcessor.destination, CliProcessor.outputPathList.First());
                    Console.WriteLine("已成功完成，导出共 {0} 条数据", result.Count);
                }

                sw.Stop();


                Console.WriteLine("Time taken: {0}", sw.Elapsed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}未知错误:{0}{1}", Environment.NewLine, ex);
                Environment.ExitCode = 2;
            }

            if (CliProcessor.waitAtEnd)
            {
                Console.WriteLine("{0}{0}敲击回车退出程序", Environment.NewLine);
                Console.ReadLine();
            }
        }
    }
}
