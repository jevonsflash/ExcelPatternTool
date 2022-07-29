using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ExcelPatternTool.Core.Excel.Core;
using ExcelPatternTool.Core.Excel.Models;
using ExcelPatternTool.Core.Excel.Models.Interfaces;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core.EntityProxy;
using System.Collections;

namespace ExcelPatternTool.Helper
{
    public class DocProcessor
    {

        public static void SaveTo<T>(string filePath, IList<T> src, ExportOption exportOption) where T : IExcelEntity
        {
            var extension = Path.GetExtension(filePath).ToLower();
            Exporter exporter = new Exporter();
            if (extension.EndsWith("xls"))
            {
                exporter.DumpXls(filePath);

            }
            else if (extension.EndsWith("xlsx"))
            {
                exporter.DumpXlsx(filePath);

            }
            else
            {
                Console.WriteLine(filePath + " 文件格式不合法");

            }
            var buff = exporter.ProcessGetBytes(src, exportOption);
            FileStream fs;
            try
            {
                fs = new FileStream(filePath, FileMode.Create);
                fs.Write(buff, 0, buff.Length);
                fs.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(filePath + " 此文件正被其他程序占用");
            }


        }

        public static void SaveTo(Type entityType, string filePath, IList src, ExportOption exportOption)
        {

            var extension = Path.GetExtension(filePath).ToLower();
            Exporter exporter = new Exporter();
            if (extension.EndsWith("xls"))
            {
                exporter.DumpXls(filePath);

            }
            else if (extension.EndsWith("xlsx"))
            {
                exporter.DumpXlsx(filePath);

            }
            else
            {
                Console.WriteLine(filePath + " 文件格式不合法");

            }
            var buff = exporter.ProcessGetBytes(entityType, src, exportOption);
            FileStream fs;
            try
            {
                fs = new FileStream(filePath, FileMode.Create);
                fs.Write(buff, 0, buff.Length);
                fs.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(filePath + " 此文件正被其他程序占用");
            }


        }

       
        public static List<T> ImportFrom<T>(string filePath, ImportOption importOption) where T : IExcelEntity
        {
            if (importOption == null)
            {
                importOption = new ImportOption(EntityProxyContainer.Current.EntityType, 0, 0);
            }
            Importer import = new Importer();
            List<T> output = new List<T>();

            var data1 = new byte[0];
            try
            {
                data1 = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(filePath + " 此文件正被其他程序占用");
                return output;
            }

            try
            {
                if (filePath.EndsWith(".xlsx"))
                {
                    import.LoadXlsx(data1);
                }
                else if (filePath.EndsWith(".xls"))
                {
                    import.LoadXls(data1);
                }
                else
                {
                    Console.WriteLine(filePath + " 文件类型错误");
                    return output;
                }

                output = import.Process<T>(importOption).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(filePath + " 格式错误");
            }

            return output;


        }

        public static List<IExcelEntity> ImportFrom(string filePath, ImportOption importOption)
        {
            if (importOption == null)
            {
                importOption = new ImportOption(EntityProxyContainer.Current.EntityType, 0, 0);
            }
            Importer import = new Importer();
            List<IExcelEntity> output = new List<IExcelEntity>();

            var data1 = new byte[0];
            try
            {
                data1 = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(filePath + " 此文件正被其他程序占用");
                return output;
            }

            try
            {
                if (filePath.EndsWith(".xlsx"))
                {
                    import.LoadXlsx(data1);
                }
                else if (filePath.EndsWith(".xls"))
                {
                    import.LoadXls(data1);
                }
                else
                {
                    Console.WriteLine(filePath + " 文件类型错误");
                    return output;
                }

                output = import.Process(EntityProxyContainer.Current.EntityType, importOption).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(filePath + " 格式错误");
            }

            return output;


        }


        public static dynamic ImportFromPathDelegator(string filePath, Func<Importer, dynamic> action)
        {
            Importer import = new Importer();

            var data1 = new byte[0];
            try
            {
                data1 = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(filePath + " 此文件正被其他程序占用");
                return default;
            }

            try
            {
                if (filePath.EndsWith(".xlsx"))
                {
                    import.LoadXlsx(data1);
                }
                else if (filePath.EndsWith(".xls"))
                {
                    import.LoadXls(data1);
                }
                else
                {
                    Console.WriteLine(filePath + " 文件类型错误");
                    return default;
                }

                return action?.Invoke(import);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var msg = $"{filePath} 格式错误\n 错误信息：{e.Message}";
                Console.WriteLine(msg);
            }

            return default;
        }


    }
}
