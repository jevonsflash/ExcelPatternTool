using System;

namespace Workshop.Infrastructure.ExcelHandler
{
    internal class Column
    {
        public string ColumnName { get; set; }
        public string PropName { get; set; }
        public Type PropType { get; set; }
        public int ColumnOrder { get; set; }
        public bool Ignore { get; set; }

        public Column()
        {
            Ignore = false;
        }
    }
}
