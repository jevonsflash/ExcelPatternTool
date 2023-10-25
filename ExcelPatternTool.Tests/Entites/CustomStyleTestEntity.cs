using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;
using ExcelPatternTool.Contracts.Attributes;
using NPOI.SS.UserModel;

namespace ExcelPatternTool.Tests.Entites
{
    public class CustomStyleTestEntity : IExcelEntity
    {
        [Exportable(ignore: true)]
        public Guid Id { get; set; }

        [Exportable(ignore: true)]
        [Importable(ignore: true)]
        public long RowNumber { get; set; }
        [Exportable("常规")]
        [Importable(0)]

        public string StringValue { get; set; }
        [Exportable("字体颜色")]
        [Importable(1)]
        [Style(FontColor = "Red")]
        public string StringValueWithStyle1 { get; set; }

        [Exportable("背景颜色")]
        [Importable(2)]
        [Style(BackColor = "Yellow")]
        public string StringValueWithStyle2 { get; set; }

        [Exportable("边框颜色")]
        [Importable(3)]
        [Style(BorderColor = "Green")]
        public string StringValueWithStyle3 { get; set; }

        [Exportable("字体")]
        [Importable(4)]
        [Style(FontName = "黑体")]
        public string StringValueWithStyle4 { get; set; }

        [Exportable("字号")]
        [Importable(5)]
        [Style(FontSize = 16)]
        public string StringValueWithStyle5 { get; set; }

        [Exportable("加粗")]
        [Importable(6)]
        [Style(IsBold = true)]
        public string StringValueWithStyle6 { get; set; }

        [Exportable("下划线")]
        [Importable(7)]
        [Style(Underline = FontUnderlineType.Single)]
        public string StringValueWithStyle7 { get; set; }

        [Exportable("斜体")]
        [Importable(8)]
        [Style(IsItalic = true)]
        public string StringValueWithStyle8 { get; set; }


        [Exportable("删除线")]
        [Importable(9)]
        [Style(IsStrikeout = true)]
        public string StringValueWithStyle9 { get; set; }





    }
}
