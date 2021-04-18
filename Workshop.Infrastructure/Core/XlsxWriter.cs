using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NPOI.OpenXml4Net.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Workshop.Infrastructure.Attributes;
using Workshop.Infrastructure.Interfaces;
using Workshop.Infrastructure.Models;

namespace Workshop.Infrastructure.Core
{
    public class XlsxWriter : BaseWriter, IWriter
    {
        MemoryStream memoryStream;
        public XSSFWorkbook document;
        private ISheet sheet;
        public XlsxWriter()
        {
            memoryStream = new MemoryStream();
            document = new XSSFWorkbook();
            base.Document = document;

        }



        public Stream WriteRows<T>(IEnumerable<T> dataCollection, string SheetName, int rowsToSkip, bool genHeader)
        {
            sheet = this.document.CreateSheet(SheetName);
            var columnMetas = GetTypeDefinition(typeof(T));

            int firstRow = sheet.FirstRowNum;
            int lastRow = dataCollection.Count();


            //rows
            int row = firstRow + rowsToSkip;
            foreach (var data in dataCollection)
            {
                IRow fila = sheet.CreateRow(row);

                SetDataToRow(rowsToSkip, genHeader, columnMetas, fila, data, row);
                row += 1;
            }

            //Set autowidth
            int col = 0;
            row = firstRow + rowsToSkip;
            foreach (var cell in sheet.GetRow(row).Cells)
            {
                sheet.AutoSizeColumn(col);
                col++;
            }



            document.Write(memoryStream);

            //Fix memorystream closed by NPOI for XLSX format
            if (!memoryStream.CanRead)
            {
                MemoryStream newMemoryStream = new MemoryStream(memoryStream.ToArray());
                newMemoryStream.Position = 0;
                return newMemoryStream;
            }
            else
            {
                memoryStream.Position = 0;
                return memoryStream;

            }

        }

    }
}
