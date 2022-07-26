using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Core.Excel.Models;

namespace ExcelPatternTool.Core.Excel.Core.AdvancedTypes
{
    public interface IFullAdvancedType : ICommentedType, IFormulatedType, IAdvancedType
    {
        StyleMetadata StyleMetadata { get; set; }

    }
}
