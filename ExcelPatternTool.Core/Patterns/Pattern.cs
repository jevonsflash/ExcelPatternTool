using ExcelPatternTool.Contracts.Patterns;
using ExcelPatternTool.Contracts.Validations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

namespace ExcelPatternTool.Core.Patterns
{
    public class Pattern
    {
        public Excel ExcelImport { get; set; }
        public Excel ExcelExport { get; set; }
        public DbExport DbExport { get; set; }
        public IEnumerable<PatternItem> Patterns { get; set; }

    }

    public class PatternItem: IValidationContainer
    {
        public string PropName { get; set; }
        public string HeaderName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PropertyType PropType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CellType CellType { get; set; }

        public bool Ignore { get; set; }
        [Range(0, int.MaxValue)]
        public int Order { get; set; }
        public IValidation Validation { get; set; }
        public ExcelExportFeild ExcelExportItem { get; set; }
    }
}
