using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Workshop.Core.Excel.Core.AdvancedTypes;

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
        public StyledType<string> Name { get; set; }
        [DisplayName("身份证号码")]
        public StyledType<string> IDCard { get; set; }
        [DisplayName("工资等级")]
        public string Level { get; set; }
        [DisplayName("工作性质")]
        public string JobCate { get; set; }

        [DisplayName("员工银行账号信息")]
        public EmployeeAccount EmployeeAccount { get; set; }
        [DisplayName("员工薪酬信息")]
        public EmployeeSalay EmployeeSalay { get; set; }
        [DisplayName("员工社保信息")]
        public EmployeeSocialInsuranceAndFund EmployeeSocialInsuranceAndFund { get; set; }
        [DisplayName("企业社保信息")]
        public EnterpriseSocialInsuranceAndFund EnterpriseSocialInsuranceAndFund { get; set; }
        [DisplayName("员工社保详细信息")]
        public EmployeeSocialInsuranceDetail EmployeeSocialInsuranceDetail { get; set; }

    }
}
