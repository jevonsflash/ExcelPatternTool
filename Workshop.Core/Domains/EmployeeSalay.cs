using System;
using System.ComponentModel;
using Workshop.Infrastructure.Core;

namespace Workshop.Core.Domains
{
    public class EmployeeSalay : BaseDomainInfo
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DisplayName("培训试用期工资")]
        public double ProbationSalary { get; set; }
        [DisplayName("基本工资")]
        public double BasicSalary { get; set; }
        [DisplayName("技能工资")]
        public double SkillSalary { get; set; }
        [DisplayName("绩效奖金")]
        public double PerformanceBonus { get; set; }
        [DisplayName("岗位津贴")]
        public double PostAllowance { get; set; }
        [DisplayName("其他津贴")]
        public double OtherAllowances { get; set; }
        [DisplayName("销售奖励")]
        public double SalesBonus { get; set; }
        [DisplayName("奖金1")]
        public double Bonus1 { get; set; }
        [DisplayName("奖金2")]
        public double Bonus2 { get; set; }
        [DisplayName("绩效奖励/扣罚")]
        public double PerformanceRewards { get; set; }
        [DisplayName("绩效扣罚")]
        public double PerformanceDeduct { get; set; }
        [DisplayName("夜班费")]
        public double NightAllowances { get; set; }
        [DisplayName("普通加班费")]
        public double OrdinaryOvertime { get; set; }
        [DisplayName("节日加班费")]
        public double HolidayOvertime { get; set; }
        [DisplayName("年限")]
        public double AgeBonus { get; set; }
        [DisplayName("考勤扣")]
        public double AttendanceDeduct { get; set; }
        [DisplayName("月全勤")]
        public double MonthlyAttendance { get; set; }
        [DisplayName("季度全勤")]
        public double QuarterlyAttendance { get; set; }
        [DisplayName("其他应发")]
        public double OtherRewards { get; set; }
        [DisplayName("其他应扣")]
        public double OtherDeduct { get; set; }
        [DisplayName("补发")]
        public double SupplementaryRewards { get; set; }
        [DisplayName("补扣")]
        public double SupplementaryDeduct { get; set; }
        [DisplayName("兼职工资")]
        public double ParttimeSalary { get; set; }
        [DisplayName("应发合计")]
        public FullAdvancedType<double> Sum { get; set; }
        [DisplayName("税后补扣\r\n(宿舍费) ")]
        public double HostelAllowances { get; set; }
        [DisplayName("税后补扣\r\n(餐费) ")]
        public double MealAllowances { get; set; }
        [DisplayName("临时计税")]
        public double TemporaryTax { get; set; }
        [DisplayName("应发+临时计税项目")]
        public FullAdvancedType<double> SumWithTemporaryTax { get; set; }
    }
}