using System;

namespace Workshop.Infrastructure.ExcelHandler
{
    public interface IImportOption
    {
        Type EntityType { get; set; }
        string SheetName { get; set; }
        int SheetNumber { get; }
        int SkipRows { get; }
    }
}