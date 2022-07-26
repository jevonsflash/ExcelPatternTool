using System;
using ExcelPatternTool.Core.Excel.Models.Interfaces;

namespace ExcelPatternTool.Core.Excel.Models
{
    public class ImportOption : IImportOption
    {
        public ImportOption(int sheetNumber, int skipRows)
        {
            SheetNumber = sheetNumber;
            SkipRows = skipRows;

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SheetNumber { get; set; }
        public int SkipRows { get; set; }
    }


    public class ImportOption<T> : IImportOption
    {
        public ImportOption(int sheetNumber, int skipRows)
        {
            SheetNumber = sheetNumber;
            SkipRows = skipRows;
            EntityType = typeof(T);

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SheetNumber { get; set; }
        public int SkipRows { get; set; }
    }

}