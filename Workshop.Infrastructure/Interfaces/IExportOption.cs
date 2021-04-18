using System;

namespace Workshop.Infrastructure.Interfaces
{
    public interface IExportOption
    {
        Type EntityType { get; set; }
        string SheetName { get; set; }
        int SheetNumber { get; }
        int SkipRows { get; }
        bool GenHeaderRow { get; set; }
    }
}