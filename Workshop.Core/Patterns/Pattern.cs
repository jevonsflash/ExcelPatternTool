using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Core.Patterns
{
    public class Pattern
    {
        public IEnumerable<PatternItem> Patterns { get; set; }

    }

    public class PatternItem
    {

        public string PropName { get; set; }
        public string HeaderName { get; set; }
        public bool Ignore { get; set; }
        public int Order { get; set; }
        public ValidationPattern ValidationPattern { get; set; }
        public ExportablePattern ExportablePattern { get; set; }
    }
}
