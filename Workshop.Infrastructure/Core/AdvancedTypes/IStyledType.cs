using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.Models;

namespace Workshop.Infrastructure.Core
{
    public interface IStyledType : IAdvancedType
    {
        string Comment { get; set; }
        StyleMetadata StyleMetadata { get; set; }


    }
}
