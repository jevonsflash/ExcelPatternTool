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
    public class EmployeeHealthEntity : IExcelEntity
    {


        [Exportable(Ignore = true)]
        public long RowNumber { get; set; }

        [Importable(0)]
        [Exportable("姓名")]
        public string ClientName { get; set; }

        [Importable(1)]
        [Exportable("收缩压")]
        public string BloodPressure2 { get; set; }

        [Importable(2)]
        [Exportable("舒张压")]
        public string BloodPressure1 { get; set; }

        [Importable(3)]
        [Exportable("体温")]
        public string Temperature { get; set; }

    }

}
