using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Workshop.Core.Domains;
using Workshop.Core.Excel.Attributes;
using Workshop.Core.Excel.Core.AdvancedTypes;
using Workshop.Core.Excel.Models.Interfaces;

namespace Workshop.Core.Entites
{
    public class EmployeeEntity : IExcelEntity, IEntity<Guid>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Exportable(ignore:true)]
        public Guid Id { get; set; }

        [Exportable(ignore:true)]
        [Importable(ignore:true)]
        public long RowNumber { get; set; }
        [DisplayName("年")]
        [Exportable("年(Year)",0)]
        [Importable("年", 0)]
        public int Year { get; set; }
        [Style(BackColor = "#66FFFF")]
        [DisplayName("月")]
        [Exportable("月", 0)]
        [Importable("月", 1)]
        public int Mounth { get; set; }
    }
}
