using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Excel.Models;

namespace Workshop.Core.Excel.Core.AdvancedTypes
{
    public interface IStyledType : IAdvancedType
    {
        string Comment { get; set; }
        StyleMetadata StyleMetadata { get; set; }


    }
}
