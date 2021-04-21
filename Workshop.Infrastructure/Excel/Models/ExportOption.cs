using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.Interfaces;

namespace Workshop.Infrastructure.Models
{
    public class ExportOption : IExportOption
    {
        public ExportOption(int sheetNumber, int skipRows)
        {
            SheetNumber = sheetNumber;
            SkipRows = skipRows;

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SheetNumber { get;  set; }
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

        }

        public Type EntityType { get; set; }
        public string SheetName { get; set; }
        public int SheetNumber { get; set; }
        public int SkipRows { get; set; }
        public bool GenHeaderRow { get; set; }
    }
}
