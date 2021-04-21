using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Infrastructure.Core
{
    public interface IFormulatedType : IAdvancedType
    {
        string Formula { get; set; }

    }
}
