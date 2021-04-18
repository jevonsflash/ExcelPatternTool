using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Infrastructure.Attributes;

namespace Workshop.Model.Excel
{
    public class ProcedureTypeInfoExcel
    {
        [Importable("序号", 1)]
        public string Id { get; set; }
        [Importable("工序名称", 2)]
        public string Name { get; set; }
        [Importable("工序描述", 3)]
        public string Desc { get; set; }
        [Importable("类型", 4)]
        public string Category { get; set; }
    }
}
