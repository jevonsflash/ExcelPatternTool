using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model.Tables
{
    public class OrderInfoTable
    {
        public string job { get; set; }
        public string productTypeID { get; set; }
        public string productTypeDesc { get; set; }
        public int quantity { get; set; }
        public string note { get; set; }
        public DateTime sapPlanStartDate { get; set; }
        public DateTime sapPlanEndDate { get; set; }
        public DateTime sapReleaseDate { get; set; }
        public DateTime mesOrderTime { get; set; }
        public DateTime mesEndTime { get; set; }
    }
}
