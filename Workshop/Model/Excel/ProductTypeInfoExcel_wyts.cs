using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleExcelImport;

namespace Workshop.Model
{
    public class ProductTypeInfoExcel_wyts
    {
        [ExcelImport("序号", order = 1)]
        public string Id { get; set; }

        [ExcelImport("产品名称", order = 2)]
        public string Name { get; set; }

        [ExcelImport("产品描述", order = 3)]
        public string Desc { get; set; }

        [ExcelImport("调试", order = 4)]
        public string 调试 { get; set; }


        [ExcelImport("调试单价", order = 5)]
        public string 调试单价 { get; set; }

        [ExcelImport("老化", order = 6)]
        public string 老化 { get; set; }


        [ExcelImport("老化单价", order = 7)]
        public string 老化单价 { get; set; }

        [ExcelImport("精调1", order = 8)]
        public string 精调1 { get; set; }


        [ExcelImport("精调1单价", order = 9)]
        public string 精调1单价 { get; set; }

        [ExcelImport("精调2", order = 10)]
        public string 精调2 { get; set; }


        [ExcelImport("精调2单价", order = 11)]
        public string 精调2单价 { get; set; }

        [ExcelImport("驻波外观", order = 12)]
        public string 驻波外观 { get; set; }

        [ExcelImport("驻波外观单价", order = 13)]
        public string 驻波外观单价 { get; set; }

        [ExcelImport("封盖螺钉数", order = 14)]
        public string 封盖螺钉数 { get; set; }


        [ExcelImport("封盖单价", order = 15)]
        public string 封盖单价 { get; set; }

        [ExcelImport("写芯片", order = 16)]
        public string 写芯片 { get; set; }


        [ExcelImport("写芯片单价", order = 17)]
        public string 写芯片单价 { get; set; }

    }
}
