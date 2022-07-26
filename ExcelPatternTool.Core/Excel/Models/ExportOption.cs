using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Core.Excel.Models.Interfaces;

namespace ExcelPatternTool.Core.Excel.Models
{
    public class ExportOption : IExportOption
    {
        public ExportOption(int sheetNumber, int skipRows)
        {
            SheetNumber = sheetNumber;
            SkipRows = skipRows;
            SheetName = "未命名";
        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SheetNumber { get; set; }
        public int SkipRows { get; set; }
        public bool GenHeaderRow { get; set; }

    }


    public class ExportOption<T> : IExportOption
    {
        public ExportOption(int sheetNumber, int skipRows)
        {
            SheetNumber = sheetNumber;
            SkipRows = skipRows;
            EntityType = typeof(T);
            SheetName = "未命名";

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SheetNumber { get; set; }
        public int SkipRows { get; set; }
        public bool GenHeaderRow { get; set; }
    }
}
