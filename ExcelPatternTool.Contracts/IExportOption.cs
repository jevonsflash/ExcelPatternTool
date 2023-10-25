using System;

namespace ExcelPatternTool.Contracts
{
    public interface IExportOption
    {
        Type EntityType { get; set; }
        string SheetName { get; set; }
        int SkipRows { get; }
        bool GenHeaderRow { get; set; }

        Type StyleMapperProvider { get; set; }
    }
}