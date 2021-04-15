using Workshop.Infrastructure.ExcelHandler;

namespace Workshop.Model.Excel
{
    public class ProductTypeInfoExcel_wycs
    {
        [ExcelImport("序号", order = 1)]
        public string Id { get; set; }

        [ExcelImport("产品名称", order = 2)]
        public string Name { get; set; }

        [ExcelImport("产品描述", order = 3)]
        public string Desc { get; set; }

        [ExcelImport("升级", order = 4)]
        public string 升级 { get; set; }


        [ExcelImport("升级单价", order = 5)]
        public string 升级单价 { get; set; }

        [ExcelImport("驻波", order = 6)]
        public string 驻波 { get; set; }


        [ExcelImport("驻波单价", order = 7)]
        public string 驻波单价 { get; set; }

        [ExcelImport("测试1", order = 8)]
        public string 测试1 { get; set; }


        [ExcelImport("测试1单价", order = 9)]
        public string 测试1单价 { get; set; }

        [ExcelImport("测试2", order = 10)]
        public string 测试2 { get; set; }


        [ExcelImport("测试2单价", order = 11)]
        public string 测试2单价 { get; set; }

        [ExcelImport("测试3", order = 12)]
        public string 测试3 { get; set; }


        [ExcelImport("测试3单价", order = 13)]
        public string 测试3单价 { get; set; }

        [ExcelImport("TD检测", order = 14)]
        public string TD检测 { get; set; }

        [ExcelImport("TD单价", order = 15)]
        public string TD单价 { get; set; }

        [ExcelImport("GSM检测", order = 16)]
        public string GSM检测 { get; set; }

        [ExcelImport("GSM单价", order = 17)]
        public string GSM单价 { get; set; }

        [ExcelImport("DCS检测", order = 18)]
        public string DCS检测 { get; set; }

        [ExcelImport("DCS单价", order = 19)]
        public string DCS单价 { get; set; }


        [ExcelImport("LET检测", order = 20)]
        public string LET检测 { get; set; }

        [ExcelImport("LET单价", order = 21)]
        public string LET单价 { get; set; }


        [ExcelImport("WCDMA检测", order = 22)]
        public string WCDMA检测 { get; set; }

        [ExcelImport("WCDMA单价", order = 23)]
        public string WCDMA单价 { get; set; }


        [ExcelImport("CDMA2000检测", order = 24)]
        public string CDMA2000检测 { get; set; }

        [ExcelImport("CDMA2000单价", order = 25)]
        public string CDMA2000单价 { get; set; }


        [ExcelImport("FDD", order = 26)]
        public string FDD { get; set; }

        [ExcelImport("FDD单价", order = 27)]
        public string FDD单价 { get; set; }


        [ExcelImport("出场检查", order = 28)]
        public string 出场检查 { get; set; }


        [ExcelImport("出场检查单价", order = 29)]
        public string 出场检查单价 { get; set; }

        [ExcelImport("监控", order = 30)]
        public string 监控 { get; set; }


        [ExcelImport("监控单价", order = 31)]
        public string 监控单价 { get; set; }
        [ExcelImport("MODEM检查", order = 32)]
        public string MODEM检查 { get; set; }


        [ExcelImport("MODEM单价", order = 33)]
        public string MODEM单价 { get; set; }

        [ExcelImport("外观检验", order = 34)]
        public string 外观检验 { get; set; }


        [ExcelImport("外观单价", order = 35)]
        public string 外观单价 { get; set; }



    }
}
