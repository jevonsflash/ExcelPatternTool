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
        private string _format;

        public string Format
        {
            get
            {
                if (string.IsNullOrEmpty(_format))
                {
                    var format = "_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * \" - \"??_ ;_ @_ ";
                    return format;
                }

                return _format;
            }
            set { _format = value; }
        }

        public string FieldValueType { get; set; }
        public string DefaultForNullOrInvalidValues { get; set; }


        public ColumnMetadata()
        {
            Ignore = false;
        }
    }
}
