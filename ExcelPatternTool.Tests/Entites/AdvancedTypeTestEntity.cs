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
    public class AdvancedTypeTestEntity : IExcelEntity
    {
        [Exportable(ignore: true)]
        public Guid Id { get; set; }

        [Exportable(ignore: true)]
        [Importable(ignore: true)]
        public long RowNumber { get; set; }
        [DisplayName("常规")]
        [Exportable("常规", Order = 0)]
        [Importable(0)]
        public string StringValue { get; set; }
        [DisplayName("注释")]
        [Exportable("注释",Order =1)]
        [Importable(1)]
        public CommentedType<string> StringWithNoteValue { get; set; }


        [DisplayName("公式")]
        [Exportable("公式", Order = 2)]
        [Importable(2)]
        public FormulatedType<string> StringWithFormulaValue { get; set; }

        [DisplayName("样式")]
        [Exportable("样式", Order = 3)]
        [Importable(3)]
        public StyledType<string> StringWithStyleValue { get; set; }

        [DisplayName("全")]
        [Exportable("全", Order = 4)]
        [Importable(4)]
        public FullAdvancedType<string> StringWithFullValue { get; set; }



    }
}
