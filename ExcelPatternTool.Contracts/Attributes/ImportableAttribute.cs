using System;

namespace ExcelPatternTool.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ImportableAttribute : Attribute
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public bool Ignore { get; set; }

        public ImportableAttribute(int order)
        {
            Order = order;
            Ignore = false;
        }

        public ImportableAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }
    }
}
