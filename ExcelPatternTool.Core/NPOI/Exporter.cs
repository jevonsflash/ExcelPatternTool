using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;
using ExcelPatternTool.Contracts;
using NPOI.SS.Formula.Functions;
using ExcelPatternTool.Core.StyleMapping;

namespace ExcelPatternTool.Core.NPOI
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
            StyleMapper styleMapper = null;
            if (exportOption.StyleMapperProvider != null)
            {
                var styleMapperProvider = (StyleMapperProvider)Activator.CreateInstance(exportOption.StyleMapperProvider);
                styleMapper = new StyleMapper(styleMapperProvider);
            }
            var stream = excelWriter.WriteRows(source, exportOption.SheetName, exportOption.SkipRows, exportOption.GenHeaderRow, styleMapper);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            return bytes;
        }


        public byte[] ProcessGetBytes(Type entityType, IEnumerable source, IExportOption exportOption)
        {
            StyleMapper styleMapper = null;
            if (exportOption.StyleMapperProvider != null)
            {
                var styleMapperProvider = (StyleMapperProvider)Activator.CreateInstance(exportOption.StyleMapperProvider);
                styleMapper = new StyleMapper(styleMapperProvider);
            }

            var stream = excelWriter.WriteRows(entityType, source, exportOption.SheetName, exportOption.SkipRows, exportOption.GenHeaderRow, styleMapper);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            return bytes;
        }


        public bool Process<T>(IEnumerable<T> source, IExportOption exportOption)
        {
            try
            {
                StyleMapper styleMapper = null;
                if (exportOption.StyleMapperProvider != null)
                {
                    var styleMapperProvider = (StyleMapperProvider)Activator.CreateInstance(exportOption.StyleMapperProvider);
                    styleMapper = new StyleMapper(styleMapperProvider);
                }
                var stream = excelWriter.WriteRows(source, exportOption.SheetName, exportOption.SkipRows, exportOption.GenHeaderRow, styleMapper);

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
