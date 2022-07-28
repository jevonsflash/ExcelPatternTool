using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool
{
    partial class CliProcessor
    {
        public static List<string> inputPathList;
        public static List<string> outputPathList;
        public static string destination;
        public static string source;
        public static bool waitAtEnd;
        public static string patternFilePath;

        public static void Usage()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine();
            Console.WriteLine("Excel Pattern Tool v{0}.{1}", versionInfo.FileMajorPart, versionInfo.FileMinorPart);
            Console.WriteLine("参数:");
            Console.WriteLine(" -p  PatternFile");
            Console.WriteLine("     指定一个Pattern文件(Json), 作为转换的模型文件");
            Console.WriteLine(" -i  Input");
            Console.WriteLine("     指定一个Excel文件路径，此文件将作为导入数据源");
            Console.WriteLine("     支持Xls或者Xlsx文件");
            Console.WriteLine(" -o  Output");
            Console.WriteLine("     指定一个路径，或Sql连接字符串作为导出目标");
            Console.WriteLine("     当指定 -d 参数为sqlserver, sqlite, mysql时，需指定为连接字符串;");
            Console.WriteLine("     当指定 -d 参数为excel时，需指定为将要另存的Excel文件路径，支持Xls或者Xlsx文件");
            Console.WriteLine(" -s  Source");
            Console.WriteLine("     值为excel");
            Console.WriteLine(" -d  Destination");
            Console.WriteLine("     值为excel, sqlserver, sqlite或者mysql");
            Console.WriteLine(" -w  WaitAtEnd");
            Console.WriteLine("     指定时，程序执行完成后，将等待用户输入退出");
            Console.WriteLine(" -h  Help");
            Console.WriteLine("     查看帮助");
        }


        public static bool ProcessCommandLine(string[] args)
        {
            inputPathList = new List<string>();
            outputPathList = new List<string>();
            destination = string.Empty;
            source = string.Empty;
            var i = 0;
            while (i < args.Length)
            {
                var arg = args[i];
                if (arg.StartsWith("/") || arg.StartsWith("-"))
                    arg = arg.Substring(1);
                switch (arg.ToLowerInvariant())
                {

                    case "p":
                        i++;
                        if (i < args.Length)
                        {
                            if (!File.Exists(args[i]))
                            {
                                Console.WriteLine("文件 '{0}' 不存在", args[i]);
                                return false;
                            }
                            patternFilePath= args[i];
                        }
                        else
                            return false;
                        break;
                    case "i":
                        i++;
                        if (i < args.Length)
                        {
                            if (!File.Exists(args[i]))
                            {
                                Console.WriteLine("文件 '{0}' 不存在", args[i]);
                                return false;
                            }
                            inputPathList.Add(args[i]);
                        }
                        else
                            return false;
                        break;
                    case "s":
                        i++;
                        if (i < args.Length)
                        {

                            if (!new string[] { "excel" }.Any(c => c==args[i]))
                            {
                                Console.WriteLine("参数值 '{0}' 不合法", args[i]);
                                return false;
                            }
                            source= args[i];

                        }
                        else
                            return false;
                        break;
                    case "o":
                        i++;
                        if (i < args.Length)
                        {
                            if (args[i].IndexOfAny(System.IO.Path.GetInvalidPathChars())>=0)
                            {
                                Console.WriteLine("路径 '{0}' 不合法", args[i]);
                                return false;
                            }
                            outputPathList.Add(args[i]);
                        }
                        else
                            return false;
                        break;
                    case "d":
                        i++;
                        if (i < args.Length)
                        {
                            if (!new string[] { "excel", "sqlserver", "sqlite", "mysql" }.Any(c => c==args[i]))
                            {
                                Console.WriteLine("参数值 '{0}' 不合法", args[i]);
                                return false;
                            }
                            destination= args[i];
                        }
                        else
                            return false;
                        break;
                    case "w":
                        waitAtEnd = true;
                        break;
                    case "h":
                        Usage();
                        return false;


                    default:
                        Console.WriteLine("无法识别的参数: {0}", args[i]);
                        break;
                }
                i++;
            }
            return !string.IsNullOrEmpty(patternFilePath)
                &&!string.IsNullOrEmpty(source)
                &&!string.IsNullOrEmpty(destination)
                &&inputPathList.Count > 0
                && outputPathList.Count > 0;

        }
    }
}