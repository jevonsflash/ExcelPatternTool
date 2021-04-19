using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Infrastructure.Core
{
    public interface ICommentedType : IAdvancedType
    {
        string Comment { get; set; }

    }
}
