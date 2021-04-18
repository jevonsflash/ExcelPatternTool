using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Infrastructure.Attributes;

namespace Workshop.Model.Excel
{
    public class OrderInfoExcel
    {
        [Importable("序号", 1)]
        public string Id { get; set; }
        [Importable("扫描时间", 2)]
        public DateTime CreateTime { get; set; }
        [Importable("姓名", 3)]
        public string UserName { get; set; }
        [Importable("工序", 4)]
        public string ProcedureName { get; set; }
        [Importable("序列号", 5)]
        public string SerialNumber { get; set; }
        [Importable("作业单", 6)]
        public string OrderNumber { get; set; }

        [Importable("产品型号", 7)]
        public string ProductModelNumber { get; set; }

        [Importable("产品描述", 8)]
        public string ProductDetail { get; set; }
        [Importable("备注", 9)]

        public string Note { get; set; }

    }
}
