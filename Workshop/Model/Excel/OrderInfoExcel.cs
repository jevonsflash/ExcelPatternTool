using SimpleExcelImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model.Excel
{
    public class OrderInfoExcel
    {
        [ExcelImport("序号", order = 1)]
        public string Id { get; set; }
        [ExcelImport("扫描时间", order = 2)]
        public DateTime CreateTime { get; set; }
        [ExcelImport("姓名", order = 3)]
        public string UserName { get; set; }
        [ExcelImport("工序", order = 4)]
        public string ProcedureName { get; set; }
        [ExcelImport("序列号", order = 5)]
        public string SerialNumber { get; set; }
        [ExcelImport("作业单", order = 6)]
        public string OrderNumber { get; set; }

        [ExcelImport("产品型号", order = 7)]
        public string ProductModelNumber { get; set; }

        [ExcelImport("产品描述", order = 8)]
        public string ProductDetail { get; set; }
        [ExcelImport("备注", order = 9)]

        public string Note { get; set; }

    }
}
