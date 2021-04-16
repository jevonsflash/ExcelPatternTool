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
        public FormulatedType<string> AccountBankName { get; set; }
        [ExcelImport("银行地点", order = 14)]
        public FormulatedType<string> AccountBankLoc { get; set; }

        [ExcelImport("个人社保号", order = 15)]
        public string SocialInsuranceNum { get; set; }
        [ExcelImport("培训试用期工资",order = 16)]
        public double ProbationSalary { get; set; }
        [ExcelImport("基本工资",order = 17)]
        public double BasicSalary { get; set; }
        [ExcelImport("技能工资",order = 18)]
        public double SkillSalary { get; set; }
        [ExcelImport("绩效奖金",order = 19)]
        public double PerformanceBonus { get; set; }
        [ExcelImport("岗位津贴",order = 20)]
        public double PostAllowance { get; set; }
        [ExcelImport("其他津贴",order = 21)]
        public double OtherAllowances { get; set; }
        [ExcelImport("销售奖励",order = 22)]
        public double SalesBonus { get; set; }
        [ExcelImport("奖金1",order = 23)]
        public double Bonus1 { get; set; }
        [ExcelImport("奖金2",order = 24)]
        public double Bonus2 { get; set; }
        [ExcelImport("绩效奖励/扣罚",order = 25)]
        public double PerformanceRewards { get; set; }
        [ExcelImport("绩效扣罚",order = 26)]
        public double PerformanceDeduct { get; set; }
        [ExcelImport("夜班费",order = 27)]
        public double NightAllowances { get; set; }
        [ExcelImport("普通加班费",order = 28)]
        public double OrdinaryOvertime { get; set; }
        [ExcelImport("节日加班费",order = 29)]
        public double HolidayOvertime { get; set; }
        [ExcelImport("年限",order = 30)]
        public double AgeBonus { get; set; }
        [ExcelImport("考勤扣",order = 31)]
        public double AttendanceDeduct { get; set; }
        [ExcelImport("月全勤",order = 32)]
        public double MonthlyAttendance { get; set; }
        [ExcelImport("季度全勤",order = 33)]
        public double QuarterlyAttendance { get; set; }
        [ExcelImport("其他应发",order = 34)]
        public double OtherRewards { get; set; }
        [ExcelImport("其他应扣",order = 35)]
        public double OtherDeduct { get; set; }
        [ExcelImport("补发",order = 36)]
        public double SupplementaryRewards { get; set; }
        [ExcelImport("补扣",order = 37)]
        public double SupplementaryDeduct { get; set; }
        [ExcelImport("兼职工资",order = 38)]
        public double ParttimeSalary { get; set; }
        [ExcelImport("应发合计",order = 39)]
        public double Sum { get; set; }
        [ExcelImport("税后补扣\r\n(宿舍费) ",order = 40)]
        public double HostelAllowances { get; set; }
        [ExcelImport("税后补扣\r\n(餐费) ",order = 41)]
        public double MealAllowances { get; set; }
        [ExcelImport("临时计税",order = 42)]
        public double TemporaryTax { get; set; }
        [ExcelImport("应发+临时计税项目",order = 43)]
        public double SumWithTemporaryTax { get; set; }
        [ExcelImport("社保个人",order = 44)]
        public double SocialInsurancePersonal { get; set; }
        [ExcelImport("补社保个人",order = 45)]
        public double SupplementarySocialInsurancePersonal { get; set; }
        [ExcelImport("公积金个人",order = 46)]
        public double ProvidentFundPersonal { get; set; }
        [ExcelImport("补公积金个人",order = 47)]
        public double SupplementaryProvidentFundPersonal { get; set; }
        [ExcelImport("税后补退",order = 48)]
        public double BeforeFillingOut { get; set; }
        [ExcelImport("个人所得税",order = 49)]
        public double PersonalIncomeTax { get; set; }
        [ExcelImport("工会费个人",order = 50)]
        public double UnionFeePersonal { get; set; }
        [ExcelImport("补扣工会费个人",order = 51)]
        public double SupplementaryUnionFeeDeductPersonal { get; set; }
        [ExcelImport("补扣福利商保",order = 52)]
        public double SupplementaryCommercialInsuranceDeduct { get; set; }
        [ExcelImport("实发薪金",order = 53)]
        public double Sum1 { get; set; }
        [ExcelImport("社保企业",order = 54)]
        public double SocialInsuranceEnterprise { get; set; }
        [ExcelImport("补社保企业",order = 55)]
        public double SupplementarySocialInsuranceEnterprise { get; set; }
        [ExcelImport("公积金企业",order = 56)]
        public double ProvidentFundEnterprise { get; set; }
        [ExcelImport("补公积金企业",order = 57)]
        public double SupplementaryProvidentFundEnterprise { get; set; }
        [ExcelImport("工会费企业",order = 58)]
        public double UnionFeeEnterprise { get; set; }
        [ExcelImport("补扣工会费企业",order = 59)]
        public double SupplementaryUnionFeeDeductEnterprise { get; set; }
        [ExcelImport("总支付",order = 60)]
        public double Sum2 { get; set; }
        [ExcelImport("基本养老险保险费（个人）",order = 61)]
        public double BasicOldAgeInsurance { get; set; }
        [ExcelImport("基本医疗保险费（个人）",order = 62)]
        public double BasicMedicalInsurance { get; set; }
        [ExcelImport("失业保险费（个人）",order = 63)]
        public double UnemploymentInsurance { get; set; }
        [ExcelImport("验算（个人）",order = 64)]
        public double Check { get; set; }
        [ExcelImport("公积金（个人）",order = 65)]
        public double ProvidentFund { get; set; }
    }
}
