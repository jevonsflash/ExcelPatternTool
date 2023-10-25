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
    public class WriteRowTestEntity : IExcelEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Exportable(ignore: true)]
        public Guid Id { get; set; }

        [Exportable(ignore: true)]
        [Importable(ignore: true)]
        public long RowNumber { get; set; }
        [DisplayName("常规")]
        [Exportable("常规", Order = 0)]
        [Importable(0)]
        public string StringValue { get; set; }
        [DisplayName("日期")]
        [Exportable("日期", Order = 1, Format = "yyyy\"年\"m\"月\"d\"日\";@")]
        [Importable(1)]
        public DateTime DateTimeValue { get; set; }

        [DisplayName("整数")]
        [Exportable("整数", Order = 2)]
        [Importable(2)]
        public int IntValue { get; set; }

        [DisplayName("小数")]
        [Exportable("小数", Order = 3)]
        [Importable(3)]
        public double DoubleValue { get; set; }


        [DisplayName("布尔值")]
        [Exportable("布尔值", Order = 4)]
        [Importable(4)]
        public bool BoolValue { get; set; }



        [DisplayName("公式参数1")]
        [Exportable("公式参数1", Order = 5)]
        [Importable(5)]
        public int FormulaArg1 { get; set; }


        [DisplayName("公式参数2")]
        [Exportable("公式参数2", Order = 6)]
        [Importable(6)]
        public int FormulaArg12 { get; set; }


        [DisplayName("公式")]
        [Exportable("公式", Order = 7)]
        [Importable(7)]
        public FormulatedType<int> IntWithFormula { get; set; }



    }
}
