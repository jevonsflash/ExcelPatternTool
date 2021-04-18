using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Infrastructure.Attributes
{
    [AttributeUsage(System.AttributeTargets.Property)]
    public class ExportableAttribute : Attribute
    {
        public int Order { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public string Format { get; set; }

        public bool Ignore { get; set; }
        
        public ExportableAttribute(string name,int order, bool ignore = false)
        {
            Order = order;
            Name = name;
            Ignore = ignore;

        }

    }
}
