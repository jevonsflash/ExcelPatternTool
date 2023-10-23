using System;
using System.Data;

namespace ExcelPatternTool.Contracts.Models
{
    public class ColumnMetadata
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
                    var format = "";
                    switch (PropType.Name.ToLower())
                    {

                        case "string":
                            format = "@";
                            break;
                        case "datetime":
                            format = "yyyy/m/d h:mm";
                            break;
                        case "int":
                        case "int32":
                            format = "0";
                            break;
                    
                        case "int64":
                        case "long":
                            format = "0";
                            break;
                        case "double":                      
                        case "single":
                        case "decimal":
                            format = "0.00_ ";
                            break;
                        case "boolean":
                        case "bool":
                            format = "@";
                            break;



                        default:
                            format = "_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * \" - \"??_ ;_ @_ ";

                            break;
                    }

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
