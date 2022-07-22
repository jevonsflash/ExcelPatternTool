using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Excel.Models;

namespace Workshop.Core.Excel.Core.AdvancedTypes
{
    public interface IFullAdvancedType : ICommentedType, IFormulatedType, IAdvancedType
    {
        StyleMetadata StyleMetadata { get; set; }

    }
}
