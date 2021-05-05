using System;
using System.ComponentModel;

namespace Workshop.Core.Domains
{
    public class EmployeeSocialInsuranceDetail : BaseDomainInfo
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DisplayName("基本养老险保险费（个人）")]
        public double BasicOldAgeInsurance { get; set; }
        [DisplayName("基本医疗保险费（个人）")]
        public double BasicMedicalInsurance { get; set; }
        [DisplayName("失业保险费（个人）")]
        public double UnemploymentInsurance { get; set; }
        [DisplayName("验算（个人）")]
        public double Check { get; set; }
        [DisplayName("公积金（个人）")]
        public double ProvidentFund { get; set; }
    }
}