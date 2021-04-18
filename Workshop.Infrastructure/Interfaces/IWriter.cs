﻿using System.Collections.Generic;
using System.IO;

namespace Workshop.Infrastructure.Interfaces
{
    public interface IWriter
    {
        Stream WriteRows<T>(IEnumerable<T> dataCollection, string SheetName, int rowsToSkip, bool genHeader);
    }
}