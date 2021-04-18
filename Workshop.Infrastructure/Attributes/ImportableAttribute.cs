using System;

namespace Workshop.Infrastructure.Attributes
{
    [AttributeUsage(System.AttributeTargets.Property)]
    public class ImportableAttribute : Attribute
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public bool Ignore { get; set; }

        public ImportableAttribute(string name, int order, bool ignore=false)
        {
            Name = name;
            Order = order;
            Ignore = ignore;
        }
    }
}
