using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExcelImport
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
