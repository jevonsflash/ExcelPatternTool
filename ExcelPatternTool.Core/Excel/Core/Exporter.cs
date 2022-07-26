using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExcelPatternTool.Core.Excel.Core.Interfaces;
using ExcelPatternTool.Core.Excel.Models.Interfaces;
using ExcelPatternTool.Core;

namespace ExcelPatternTool.Core.Excel.Core
{
    public class Exporter
    {
        private IWriter excelWriter;
        private string filePath;
        public bool DumpXls(string filePath)
        {
            excelWriter = new XlsWriter();
            this.filePath = filePath;
            return true;
        }

        public bool DumpXlsx(string filePath)
        {
            excelWriter = new XlsxWriter();
            this.filePath = filePath;
            return true;
        }


        public byte[] ProcessGetBytes<T>(IEnumerable<T> source, IExportOption exportOption)
        {
            var stream = excelWriter.WriteRows(source, exportOption.SheetName, exportOption.SkipRows, exportOption.GenHeaderRow);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            return bytes;
        }


        public bool Process<T>(IEnumerable<T> source, IExportOption exportOption)
        {
            try
            {
                var stream = excelWriter.WriteRows(source, exportOption.SheetName, exportOption.SkipRows, exportOption.GenHeaderRow);

                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    file.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }


            return true;
        }
    }
}
