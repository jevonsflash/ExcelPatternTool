using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Model.Excel;

namespace Workshop.Model
{
    public class BatchInfo
    {
        public string Title { get; set; }
        public List<ProductInfoExcel> ProductInfos { get; set; }
    }
}
