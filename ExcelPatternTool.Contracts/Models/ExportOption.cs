using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Contracts;

namespace ExcelPatternTool.Contracts.Models
{
    public class ExportOption : IExportOption
    {
        public ExportOption(Type entityType, int skipRows, string sheetName = "Sheet1")
        {
            SkipRows = skipRows;
            SheetName = sheetName;
            EntityType = entityType;

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SkipRows { get; set; }
        public bool GenHeaderRow { get; set; }
        public Type StyleMapperProvider { get; set; }
    }


    public class ExportOption<T> : IExportOption
    {
        public ExportOption(int skipRows, string sheetName = "Sheet1")
        {
            SkipRows = skipRows;
            EntityType = typeof(T);
            SheetName = sheetName;

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SkipRows { get; set; }
        public bool GenHeaderRow { get; set; }
        public Type StyleMapperProvider { get; set; }
    }
}
