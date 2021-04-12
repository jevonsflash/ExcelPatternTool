using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExcelImport
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ExcelImport:System.Attribute
    {
        private string name;
        public bool ignore;
        public int order;

        public ExcelImport(string name)
        {

        }

        public string GetName()
        {
            return name;
        }
        public int GetOrder()
        {
            return order;
        }
        public bool GetIgnore()
        {
            return ignore;
        }
    }
}
