using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Exportable.Engines.Excel;
using Workshop.Model;

namespace Workshop.Helper
{
    public class DocHelper
    {
        public static void SaveTo<T>(IList<T> src) where T:class
        {
            IExcelExportEngine engine = new ExcelExportEngine();
            engine.AddData(src);
            engine.SetFormat(ExcelVersion.XLS);
            MemoryStream memory = engine.Export();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = CommonHelper.DesktopPath;
            saveFileDialog.Filter = "所有文件(*.*)|*.*";
            saveFileDialog.FileName = "RootElements";
            saveFileDialog.DefaultExt = "xls";
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

    }
}
