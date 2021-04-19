using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.Models;

namespace Workshop.Infrastructure.Core
{
    public interface IFullAdvancedType : ICommentedType, IFormulatedType, IAdvancedType
    {
        StyleMetadata StyleMetadata { get; set; }

    }
}
