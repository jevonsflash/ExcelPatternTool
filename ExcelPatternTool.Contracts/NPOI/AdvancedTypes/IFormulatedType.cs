using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelPatternTool.Contracts.NPOI.AdvancedTypes
{
    public interface IFormulatedType : IAdvancedType
    {
        string Formula { get; set; }

    }
}
