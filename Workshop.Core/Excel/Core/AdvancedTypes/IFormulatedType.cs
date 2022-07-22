using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Core.Excel.Core.AdvancedTypes
{
    public interface IFormulatedType : IAdvancedType
    {
        string Formula { get; set; }

    }
}
