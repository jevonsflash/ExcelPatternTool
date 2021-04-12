using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model
{
    public class ProductResult
    {
        public ProductResult(bool isSuccess, ErrorType type = ErrorType.解析失败)
        {
            this.IsSuccess = isSuccess;
            this.Type = type;
        }
        public bool IsSuccess { get; set; }
        public ErrorType Type { get; set; }
        public object arg { get; set; }
    }
}
