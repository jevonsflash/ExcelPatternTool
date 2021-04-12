using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExcelImport
{
    internal class BaseExcelReader
    {
        internal List<Column> GetTypeDefinition(Type type)
        {
            List<Column> columns = new List<Column>();
            foreach (var prop in type.GetProperties())
            {
                var tmp = new Column();
                var attrs = System.Attribute.GetCustomAttributes(prop);
                tmp.PropName = prop.Name;
                tmp.PropType = prop.PropertyType;
                tmp.ColumnName = prop.Name;
                tmp.ColumnOrder = 0;
                foreach (var attr in attrs)
                {
                    if (attr is ExcelImport)
                    {
                        ExcelImport attribute = (ExcelImport)attr;
                        tmp.ColumnName = attribute.GetName();
                        tmp.ColumnOrder = attribute.order;
                        tmp.Ignore = attribute.ignore;
                    }
                }
                if (!tmp.Ignore)
                {
                    columns.Add(tmp);
                }
            }
            return columns.OrderBy(x=>x.ColumnOrder).ToList();
        }
    }
}
