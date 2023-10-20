using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelPatternTool.Contracts.NPOI.AdvancedTypes
{
    public interface ICommentedType : IAdvancedType
    {
        string Comment { get; set; }

    }
}
