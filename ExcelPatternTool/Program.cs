using ExcelPatternTool.Core.EntityProxy;
using ExcelPatternTool.Core.Excel.Models;
using ExcelPatternTool.Core.Validators;
using ExcelPatternTool.Core.Validators.Implements;
using ExcelPatternTool.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

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
                ValidateProcessor.Init();
                DbProcessor.Init();

                var sw = Stopwatch.StartNew();

                var op1 = new ImportOption(EntityProxyContainer.Current.EntityType, 0, 2);
                var result = DocProcessor.ImportFrom(CliProcessor.inputPathList.First(), op1);

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
                    DocProcessor.SaveTo(
                        EntityProxyContainer.Current.EntityType,
                        CliProcessor.outputPathList.First(),
                        result,
                        new ExportOption(1, 1) { SheetName = "Sheet1", GenHeaderRow = true });
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
