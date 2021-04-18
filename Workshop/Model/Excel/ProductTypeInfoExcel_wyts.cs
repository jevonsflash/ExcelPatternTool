using Workshop.Infrastructure.Attributes;

namespace Workshop.Model.Excel
{
    public class ProductTypeInfoExcel_wyts
    {
        [Importable("序号", 1)]
        public string Id { get; set; }

        [Importable("产品名称", 2)]
        public string Name { get; set; }

        [Importable("产品描述", 3)]
        public string Desc { get; set; }

        [Importable("调试", 4)]
        public string 调试 { get; set; }


        [Importable("调试单价", 5)]
        public string 调试单价 { get; set; }

        [Importable("老化", 6)]
        public string 老化 { get; set; }


        [Importable("老化单价", 7)]
        public string 老化单价 { get; set; }

        [Importable("精调1", 8)]
        public string 精调1 { get; set; }


        [Importable("精调1单价", 9)]
        public string 精调1单价 { get; set; }

        [Importable("精调2", 10)]
        public string 精调2 { get; set; }


        [Importable("精调2单价", 11)]
        public string 精调2单价 { get; set; }

        [Importable("驻波外观", 12)]
        public string 驻波外观 { get; set; }

        [Importable("驻波外观单价", 13)]
        public string 驻波外观单价 { get; set; }

        [Importable("封盖螺钉数", 14)]
        public string 封盖螺钉数 { get; set; }


        [Importable("封盖单价", 15)]
        public string 封盖单价 { get; set; }

        [Importable("写芯片", 16)]
        public string 写芯片 { get; set; }


        [Importable("写芯片单价", 17)]
        public string 写芯片单价 { get; set; }

    }
}
