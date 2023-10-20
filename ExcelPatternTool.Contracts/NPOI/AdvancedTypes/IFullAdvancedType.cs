using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Contracts.Models;

namespace ExcelPatternTool.Contracts.NPOI.AdvancedTypes
{
    public interface IFullAdvancedType : ICommentedType, IFormulatedType, IAdvancedType
    {
        StyleMetadata StyleMetadata { get; set; }

    }
}
