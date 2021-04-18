using System;

namespace Workshop.Infrastructure.Models
{
    internal class ColumnMetadata
    {
        public string ColumnName { get; set; }
        public string PropName { get; set; }
        public Type PropType { get; set; }
        public int ColumnOrder { get; set; }
        public bool Ignore { get; set; }

        public string Format { get; set; }
        public string FieldValueType { get; set; }
        public string DefaultForNullOrInvalidValues { get; set; }


        public ColumnMetadata()
        {
            Ignore = false;
        }
    }
}
