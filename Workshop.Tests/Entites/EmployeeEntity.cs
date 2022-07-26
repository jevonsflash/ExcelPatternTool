using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Workshop.Core.Excel.Attributes;
using Workshop.Core.Excel.Core.AdvancedTypes;
using Workshop.Core.Excel.Models.Interfaces;

namespace Workshop.Core.Entites
{
    [Keyless]
    public class EmployeeEntity : IExcelEntity
    {
        [Exportable(ignore: true)]
        [Importable(ignore: true)]
        public long RowNumber { get; set; }
        [DisplayName("常规")]
        [Exportable("常规", 0)]
        [Importable("常规", 0)]
        public string StringValue { get; set; }
        [DisplayName("日期")]
        [Exportable("日期", 0)]
        [Importable("日期", 1)]
        public DateTime DateTimeValue { get; set; }

        [DisplayName("整数")]
        [Exportable("整数", 0)]
        [Importable("整数", 2)]
        public int IntValue { get; set; }

        [DisplayName("小数")]
        [Exportable("小数", 0)]
        [Importable("小数", 3)]
        public double DoubleValue { get; set; }


        [DisplayName("布尔值")]
        [Exportable("布尔值", 0)]
        [Importable("布尔值", 4)]
        public bool BoolValue { get; set; }

        [DisplayName("常规(注释)")]
        [Exportable("常规(注释)", 0)]
        [Importable("常规(注释)", 5)]
        public CommentedType<string> StringWithNoteValue { get; set; }

        [DisplayName("常规(样式)")]
        [Exportable("常规(样式)", 0)]
        [Importable("常规(样式)", 6)]
        public StyledType<string> StringWithStyleValue { get; set; }


        [DisplayName("公式")]
        [Exportable("公式", 0)]
        [Importable("公式", 7)]
        public FormulatedType<int> IntWithFormula { get; set; }


        
    }
}
