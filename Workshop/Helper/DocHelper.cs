using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Exportable.Engines.Excel;
using Workshop.Infrastructure.ExcelHandler;
using Workshop.Model;
using Workshop.Model.Excel;

namespace Workshop.Helper
{
    public class DocHelper
    {
        private static string _fileName = "未命名Excel";
        private static string _excelFilesXlsxXls = "Excel 2007文件|*.xlsx|Excel 99-03文件|*.xls";

        public static void SaveTo<T>(IList<T> src) where T : class
        {
            IExcelExportEngine engine = new ExcelExportEngine();
            engine.AddData(src);
            engine.SetFormat(ExcelVersion.XLSX);
            MemoryStream memory = engine.Export();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = CommonHelper.DesktopPath;
            saveFileDialog.Filter = _excelFilesXlsxXls;
            saveFileDialog.FileName = _fileName;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;
            saveFileDialog.RestoreDirectory = true;

            // Show save file dialog box
            bool? result = saveFileDialog.ShowDialog();
            //点了保存按钮进入
            if (result == true)
            {
                var aa = saveFileDialog.FileName;
                FileStream fs = new FileStream(aa, FileMode.Create);
                byte[] buff = memory.ToArray();
                fs.Write(buff, 0, buff.Length);
                fs.Close();
            }
        }

        public static IList<T> ImportFrom<T>(ImportOption importOption = null)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = CommonHelper.DesktopPath;
            openFileDialog.Filter = _excelFilesXlsxXls;
            openFileDialog.FileName = _fileName;
            openFileDialog.AddExtension = true;
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var filePath = openFileDialog.FileName;
                return ImportFromPath<T>(filePath, importOption);
            }
            else
            {
                return default;
            }
        }


        public static List<T> ImportFromPath<T>(string filePath, ImportOption importOption)
        {
            if (importOption==null)
            {
                importOption = new ImportOption(0, 1);
            }
            ImportFromExcel import = new ImportFromExcel();
            List<T> output = new List<T>();

            var data1 = new byte[0];
            try
            {
                data1 = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(filePath + " 此文件正被其他程序占用");
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
                    MessageBox.Show(filePath + " 文件类型错误");
                    return output;
                }

                output = import.ExcelToList<T>(importOption).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(filePath + " 格式错误");
            }

            return output;


        }

        public static dynamic ImportFrom(Func<ImportFromExcel,dynamic> action)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = CommonHelper.DesktopPath;
            openFileDialog.Filter = _excelFilesXlsxXls;
            openFileDialog.FileName = _fileName;
            openFileDialog.AddExtension = true;
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var filePath = openFileDialog.FileName;
                return ImporTask(filePath,action);
            }
            else
            {
                return default;
            }
        }

        public static dynamic ImporTask(string filePath, Func<ImportFromExcel, dynamic> action)
        {
            ImportFromExcel import = new ImportFromExcel();

            var data1 = new byte[0];
            try
            {
                data1 = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(filePath + " 此文件正被其他程序占用");
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
                    MessageBox.Show(filePath + " 文件类型错误");
                    return default;
                }

                return action?.Invoke(import);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(filePath + " 格式错误");
            }

            return default;
        }


    }
}
