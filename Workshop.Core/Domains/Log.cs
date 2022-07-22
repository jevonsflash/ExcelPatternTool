using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Workshop.Core;

namespace Workshop.Core.Domains
{
    public class Log : BaseDomainInfo
    {
        public const string IMPORT = "导入";
        public const string OUTPUT = "导出";
        public const string CHECK = "验证";

        public Log(string type, string result, int count):base()
        {
            this.Type = type;
            this.Result = result;
            this.Count = count;
        }

        [DisplayName("操作类型")]
        public string Type { get; set; }
        [DisplayName("结果")]
        public string Result { get; set; }
        [DisplayName("条目数量")]
        public int Count { get; set; }
    }
}
