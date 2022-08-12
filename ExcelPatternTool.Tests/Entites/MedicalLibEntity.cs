using ExcelPatternTool.Core.Excel.Attributes;
using ExcelPatternTool.Core.Excel.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool.Tests.Entites
{
    public class MedicalLibEntity : IExcelEntity
    {

        [Importable(ignore: true)]
        public long RowNumber { get; set; }


        [Exportable("YPID")]
        [Importable(1)]
        public string Code { get; set; }

        [Exportable("产品名称")]
        [Importable(2)]
        public string Name { get; set; }



        [Exportable("生产企业名称")]
        [Importable(3)]
        public string Manufacturer { get; set; }

        [Exportable("剂型分类码")]
        [Importable(16)]
        public string TypeCode { get; set; }


    }
}
