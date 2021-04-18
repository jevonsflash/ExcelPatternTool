using Workshop.Infrastructure.Attributes;

namespace Workshop.Model.Excel
{
    public class ProductTypeInfoExcel_wycs
    {
        [Importable("序号", 1)]
        public string Id { get; set; }

        [Importable("产品名称", 2)]
        public string Name { get; set; }

        [Importable("产品描述", 3)]
        public string Desc { get; set; }

        [Importable("升级", 4)]
        public string 升级 { get; set; }


        [Importable("升级单价", 5)]
        public string 升级单价 { get; set; }

        [Importable("驻波", 6)]
        public string 驻波 { get; set; }


        [Importable("驻波单价", 7)]
        public string 驻波单价 { get; set; }

        [Importable("测试1", 8)]
        public string 测试1 { get; set; }


        [Importable("测试1单价", 9)]
        public string 测试1单价 { get; set; }

        [Importable("测试2", 10)]
        public string 测试2 { get; set; }


        [Importable("测试2单价", 11)]
        public string 测试2单价 { get; set; }

        [Importable("测试3", 12)]
        public string 测试3 { get; set; }


        [Importable("测试3单价", 13)]
        public string 测试3单价 { get; set; }

        [Importable("TD检测", 14)]
        public string TD检测 { get; set; }

        [Importable("TD单价", 15)]
        public string TD单价 { get; set; }

        [Importable("GSM检测", 16)]
        public string GSM检测 { get; set; }

        [Importable("GSM单价", 17)]
        public string GSM单价 { get; set; }

        [Importable("DCS检测", 18)]
        public string DCS检测 { get; set; }

        [Importable("DCS单价", 19)]
        public string DCS单价 { get; set; }


        [Importable("LET检测", 20)]
        public string LET检测 { get; set; }

        [Importable("LET单价", 21)]
        public string LET单价 { get; set; }


        [Importable("WCDMA检测", 22)]
        public string WCDMA检测 { get; set; }

        [Importable("WCDMA单价", 23)]
        public string WCDMA单价 { get; set; }


        [Importable("CDMA2000检测", 24)]
        public string CDMA2000检测 { get; set; }

        [Importable("CDMA2000单价", 25)]
        public string CDMA2000单价 { get; set; }


        [Importable("FDD", 26)]
        public string FDD { get; set; }

        [Importable("FDD单价", 27)]
        public string FDD单价 { get; set; }


        [Importable("出场检查", 28)]
        public string 出场检查 { get; set; }


        [Importable("出场检查单价", 29)]
        public string 出场检查单价 { get; set; }

        [Importable("监控", 30)]
        public string 监控 { get; set; }


        [Importable("监控单价", 31)]
        public string 监控单价 { get; set; }
        [Importable("MODEM检查", 32)]
        public string MODEM检查 { get; set; }


        [Importable("MODEM单价", 33)]
        public string MODEM单价 { get; set; }

        [Importable("外观检验", 34)]
        public string 外观检验 { get; set; }


        [Importable("外观单价", 35)]
        public string 外观单价 { get; set; }



    }
}
