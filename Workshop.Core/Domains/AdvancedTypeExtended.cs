using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Workshop.Core.Domains
{
    public class AdvancedTypeExtended : BaseDomainInfo
    {
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }
        public string AdvancedType { get; set; }
        public string Comment { get; set; }
        public string Formula { get; set; }
        public string FontColor { get; set; }
        public string FontName { get; set; }
        public short FontSize { get; set; }
        public string BorderColor { get; set; }
        public string BackColor { get; set; }
    }
}
