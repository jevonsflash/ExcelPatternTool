using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Workshop.Infrastructure.ExcelHandler;

namespace Workshop.Core.Domains
{
    public class Employee : BaseDomainInfo
    {
        [ExcelImport("年", order = 0)] 
        public int Year { get; set; }
        [ExcelImport("月", order = 1)]
        public int Mounth { get; set; }
        [ExcelImport("批次", order = 2)]
        public string Batch { get; set; }
        [ExcelImport("序号", order = 3)]
        public string SerialNum { get; set; }
        [ExcelImport("部门", order = 4)]
        public string Dept { get; set; }
        [ExcelImport("项目", order = 5)]
        public string Proj { get; set; }
        [ExcelImport("状态", order = 6)]
        public string State { get; set; }
        [ExcelImport("姓名", order = 7)]
        public string Name { get; set; }
        [ExcelImport("身份证号码", order = 8)]
        public string IDCard { get; set; }
        [ExcelImport("工资等级", order = 9)]
        public string Level { get; set; }
        [ExcelImport("工作性质", order = 10)]
        public string JobCate { get; set; }

        public EmployeeAccount EmployeeAccount { get; set; }
        public EmployeeSalay SalayInfo { get; set; }
        public EmployeeSocialInsuranceAndFund EmployeeSocialInsuranceAndFund { get; set; }
        public EnterpriseSocialInsuranceAndFund EnterpriseSocialInsuranceAndFund { get; set; }
        public EmployeeSocialInsuranceDetail EmployeeSocialInsuranceDetail { get; set; }

    }
}
