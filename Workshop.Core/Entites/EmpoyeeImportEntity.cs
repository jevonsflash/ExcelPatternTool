using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.ExcelHandler;

namespace Workshop.Core.Entites
{
    public class EmpoyeeImportEntity
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
        [ExcelImport("账号", order = 11)]
        public string AccountNum { get; set; }

        [ExcelImport("名称和地点", order = 12)]
        public string AccountBankAlias { get; set; }
        [ExcelImport("银行名称", order = 13)]
        public string AccountBankName { get; set; }
        [ExcelImport("银行地点", order = 14)]
        public string AccountBankLoc { get; set; }

        [ExcelImport("个人社保号", order = 15)]
        public string SocialInsuranceNum { get; set; }
        public string ProbationSalary { get; set; }
        public string BasicSalary { get; set; }
        public string SkillSalary { get; set; }
        public string PerformanceBonus { get; set; }
        public string PostAllowance { get; set; }
        public string OtherAllowances { get; set; }
        public string SalesBonus { get; set; }
        public string Bonus1 { get; set; }
        public string Bonus2 { get; set; }
        public string PerformanceRewards { get; set; }
        public string PerformanceDeduct { get; set; }
        public string NightAllowances { get; set; }
        public string OrdinaryOvertime { get; set; }
        public string HolidayOvertime { get; set; }
        public string AgeBonus { get; set; }
        public string AttendanceDeduct { get; set; }
        public string MonthlyAttendance { get; set; }
        public string QuarterlyAttendance { get; set; }
        public string OtherRewards { get; set; }
        public string OtherDeduct { get; set; }
        public string SupplementaryRewards { get; set; }
        public string SocialInsurancePersonal { get; set; }
        public string SupplementarySocialInsurancePersonal { get; set; }
        public string ProvidentFundPersonal { get; set; }
        public string SupplementaryProvidentFundPersonal { get; set; }
        public string BeforeFillingOut { get; set; }
        public string PersonalIncomeTax { get; set; }
        public string UnionFeePersonal { get; set; }
        public string SupplementaryUnionFeeDeductPersonal { get; set; }
        public string SupplementaryCommercialInsuranceDeduct { get; set; }
        public string Sum { get; set; }

    }
}
