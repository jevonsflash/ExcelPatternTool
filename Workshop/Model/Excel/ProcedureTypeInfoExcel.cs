using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Infrastructure.ExcelHandler;

namespace Workshop.Model.Excel
{
    public class ProcedureTypeInfoExcel
    {
        [ExcelImport("序号", order = 1)]
        public string Id { get; set; }
        [ExcelImport("工序名称", order = 2)]
        public string Name { get; set; }
        [ExcelImport("工序描述", order = 3)]
        public string Desc { get; set; }
        [ExcelImport("类型", order = 4)]
        public string Category { get; set; }
    }
}
