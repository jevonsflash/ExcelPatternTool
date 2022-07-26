using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelPatternTool.Core.Excel.Core.AdvancedTypes
{
    public interface ICommentedType : IAdvancedType
    {
        string Comment { get; set; }

    }
}
