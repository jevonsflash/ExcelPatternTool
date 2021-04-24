using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Workshop.Core.Domains
{
    public class Employee : BaseDomainInfo
    {
        [DisplayName("年")] 
        public int Year { get; set; }
        [DisplayName("月")]
        public int Mounth { get; set; }
        [DisplayName("批次")]
        public string Batch { get; set; }
        [DisplayName("序号")]
        public string SerialNum { get; set; }
        [DisplayName("部门")]
        public string Dept { get; set; }
        [DisplayName("项目")]
        public string Proj { get; set; }
        [DisplayName("状态")]
        public string State { get; set; }
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("身份证号码")]
        public string IDCard { get; set; }
        [DisplayName("工资等级")]
        public string Level { get; set; }
        [DisplayName("工作性质")]
        public string JobCate { get; set; }

        [DisplayName("工作性质")]
        public EmployeeAccount EmployeeAccount { get; set; }
        public EmployeeSalay EmployeeSalay { get; set; }
        public EmployeeSocialInsuranceAndFund EmployeeSocialInsuranceAndFund { get; set; }
        public EnterpriseSocialInsuranceAndFund EnterpriseSocialInsuranceAndFund { get; set; }
        public EmployeeSocialInsuranceDetail EmployeeSocialInsuranceDetail { get; set; }

    }
}
