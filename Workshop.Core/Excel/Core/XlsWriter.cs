using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Workshop.Core.Excel.Core.Interfaces;

namespace Workshop.Core.Excel.Core
{

    public class XlsWriter : BaseWriter, IWriter
    {
        MemoryStream memoryStream;
        private ISheet sheet;
        public XlsWriter()
        {
            memoryStream = new MemoryStream();
            Document = new HSSFWorkbook();

        }



        public Stream WriteRows<T>(IEnumerable<T> dataCollection, string SheetName, int rowsToSkip, bool genHeader)
        {
            sheet = Document.CreateSheet(SheetName);
            var columnMetas = GetTypeDefinition(typeof(T));

            int firstRow = sheet.FirstRowNum;
            int lastRow = dataCollection.Count();


            int row = firstRow + rowsToSkip;
            if (genHeader)
            {
                var headerRow = sheet.CreateRow(row);
                SetHeaderToRow<T>(rowsToSkip, columnMetas, headerRow, row);
                row += 1;
            }
            foreach (var data in dataCollection)
            {
                var ws = Stopwatch.StartNew();
                ws.Start();
                var newRow = sheet.CreateRow(row);
                SetDataToRow(columnMetas, newRow, data);
                row += 1;
                ws.Stop();
                Debug.WriteLine($"当前行循环{newRow.RowNum}耗时{ws.ElapsedMilliseconds}ms");
                ws.Reset();
            }

            int col = 0;
            row = firstRow + rowsToSkip;
            foreach (var cell in sheet.GetRow(row).Cells)
            {
                sheet.AutoSizeColumn(col);
                col++;
            }



            Document.Write(memoryStream);

            if (!memoryStream.CanRead)
            {
                MemoryStream newMemoryStream = new MemoryStream(memoryStream.ToArray());
                newMemoryStream.Position = 0;

                StyleBuilderProvider.DisposeCurrent();
                return newMemoryStream;

            }
            else
            {
                memoryStream.Position = 0;
                StyleBuilderProvider.DisposeCurrent();
                return memoryStream;

            }

        }

    }

}
