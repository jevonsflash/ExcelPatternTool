using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelPatternTool.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExportableAttribute : Attribute
    {
        public int Order { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public string Format { get; set; }



        public bool Ignore { get; set; }

        public ExportableAttribute(string name)
        {
            Name = name;
            Ignore = false;

        }
        public ExportableAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }
    }
}
