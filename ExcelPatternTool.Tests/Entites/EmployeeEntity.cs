using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;
using ExcelPatternTool.Contracts.Attributes;

namespace ExcelPatternTool.Tests.Entites
{
    [Keyless]
    [Table("EmployeeEntity")]
    public class EmployeeEntity : IExcelEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Exportable(ignore: true)]
        public Guid Id { get; set; }

        [Exportable(ignore: true)]
        [Importable(ignore: true)]
        public long RowNumber { get; set; }
        [DisplayName("常规")]
        [Exportable("常规")]
        [Importable(0)]
        public string StringValue { get; set; }
        [DisplayName("日期")]
        [Exportable("日期")]
        [Importable(1)]
        public DateTime DateTimeValue { get; set; }

        [DisplayName("整数")]
        [Exportable("整数")]
        [Importable(2)]
        public int IntValue { get; set; }

        [DisplayName("小数")]
        [Exportable("小数")]
        [Importable(3)]
        public double DoubleValue { get; set; }


        [DisplayName("布尔值")]
        [Exportable("布尔值")]
        [Importable(4)]
        public bool BoolValue { get; set; }

        [DisplayName("常规(注释)")]
        [Exportable("常规(注释)")]
        [Importable(5)]
        public CommentedType<string> StringWithNoteValue { get; set; }

        [DisplayName("常规(样式)")]
        [Exportable("常规(样式)")]
        [Importable(6)]
        public StyledType<string> StringWithStyleValue { get; set; }


        [DisplayName("公式")]
        [Exportable("公式")]
        [Importable(10)]
        public FormulatedType<int> IntWithFormula { get; set; }



    }
}
