using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.Attributes;
using Workshop.Infrastructure.Core;

namespace Workshop.Core.Entites
{
    public class EmpoyeeImportEntity
    {

        [Exportable("Year",0)]
        [Importable("年", 0)]
        public int Year { get; set; }
        [Importable("月", 1)]
        public int Mounth { get; set; }
        [Importable("批次", 2)]
        public string Batch { get; set; }
        [Importable("序号", 3)]
        public string SerialNum { get; set; }
        [Importable("部门", 4)]
        public string Dept { get; set; }
        [Importable("项目", 5)]
        public string Proj { get; set; }
        [Importable("状态", 6)]
        public string State { get; set; }
        [Importable("姓名", 7)]
        public string Name { get; set; }
        [Importable("身份证号码", 8)]
        public string IDCard { get; set; }
        [Importable("工资等级", 9)]
        public string Level { get; set; }
        [Importable("工作性质", 10)]
        public string JobCate { get; set; }
        [Importable("账号", 11)]
        public string AccountNum { get; set; }

        [Importable("名称和地点", 12)]
        public string AccountBankAlias { get; set; }
        [Importable("银行名称", 13)]
        public FormulatedType<string> AccountBankName { get; set; }
        [Importable("银行地点", 14)]
        public FormulatedType<string> AccountBankLoc { get; set; }

        [Importable("个人社保号", 15)]
        public string SocialInsuranceNum { get; set; }
        [Importable("培训试用期工资",  16)]
        public double ProbationSalary { get; set; }
        [Importable("基本工资",  17)]
        public double BasicSalary { get; set; }
        [Importable("技能工资",  18)]
        public double SkillSalary { get; set; }
        [Importable("绩效奖金",  19)]
        public double PerformanceBonus { get; set; }
        [Importable("岗位津贴",  20)]
        public double PostAllowance { get; set; }
        [Importable("其他津贴",  21)]
        public double OtherAllowances { get; set; }
        [Importable("销售奖励",  22)]
        public double SalesBonus { get; set; }
        [Importable("奖金1",  23)]
        public double Bonus1 { get; set; }
        [Importable("奖金2",  24)]
        public double Bonus2 { get; set; }
        [Importable("绩效奖励/扣罚",  25)]
        public double PerformanceRewards { get; set; }
        [Importable("绩效扣罚",  26)]
        public double PerformanceDeduct { get; set; }
        [Importable("夜班费",  27)]
        public double NightAllowances { get; set; }
        [Importable("普通加班费",  28)]
        public double OrdinaryOvertime { get; set; }
        [Importable("节日加班费",  29)]
        public double HolidayOvertime { get; set; }
        [Importable("年限",  30)]
        public double AgeBonus { get; set; }
        [Importable("考勤扣",  31)]
        public double AttendanceDeduct { get; set; }
        [Importable("月全勤",  32)]
        public double MonthlyAttendance { get; set; }
        [Importable("季度全勤",  33)]
        public double QuarterlyAttendance { get; set; }
        [Importable("其他应发",  34)]
        public double OtherRewards { get; set; }
        [Importable("其他应扣",  35)]
        public double OtherDeduct { get; set; }
        [Importable("补发",  36)]
        public double SupplementaryRewards { get; set; }
        [Importable("补扣",  37)]
        public double SupplementaryDeduct { get; set; }
        [Importable("兼职工资",  38)]
        public double ParttimeSalary { get; set; }
        [Importable("应发合计",  39)]
        public double Sum { get; set; }
        [Importable("税后补扣\r\n(宿舍费) ",  40)]
        public double HostelAllowances { get; set; }
        [Importable("税后补扣\r\n(餐费) ",  41)]
        public double MealAllowances { get; set; }
        [Importable("临时计税",  42)]
        public double TemporaryTax { get; set; }
        [Importable("应发+临时计税项目",  43)]
        public double SumWithTemporaryTax { get; set; }
        [Importable("社保个人",  44)]
        public double SocialInsurancePersonal { get; set; }
        [Importable("补社保个人",  45)]
        public double SupplementarySocialInsurancePersonal { get; set; }
        [Importable("公积金个人",  46)]
        public double ProvidentFundPersonal { get; set; }
        [Importable("补公积金个人",  47)]
        public double SupplementaryProvidentFundPersonal { get; set; }
        [Importable("税后补退",  48)]
        public double BeforeFillingOut { get; set; }
        [Importable("个人所得税",  49)]
        public double PersonalIncomeTax { get; set; }
        [Importable("工会费个人",  50)]
        public double UnionFeePersonal { get; set; }
        [Importable("补扣工会费个人",  51)]
        public double SupplementaryUnionFeeDeductPersonal { get; set; }
        [Importable("补扣福利商保",  52)]
        public double SupplementaryCommercialInsuranceDeduct { get; set; }
        [Importable("实发薪金",  53)]
        public double Sum1 { get; set; }
        [Importable("社保企业",  54)]
        public double SocialInsuranceEnterprise { get; set; }
        [Importable("补社保企业",  55)]
        public double SupplementarySocialInsuranceEnterprise { get; set; }
        [Importable("公积金企业",  56)]
        public double ProvidentFundEnterprise { get; set; }
        [Importable("补公积金企业",  57)]
        public double SupplementaryProvidentFundEnterprise { get; set; }
        [Importable("工会费企业",  58)]
        public double UnionFeeEnterprise { get; set; }
        [Importable("补扣工会费企业",  59)]
        public double SupplementaryUnionFeeDeductEnterprise { get; set; }
        [Importable("总支付",  60)]
        public double Sum2 { get; set; }
        [Importable("基本养老险保险费（个人）",  61)]
        public double BasicOldAgeInsurance { get; set; }
        [Importable("基本医疗保险费（个人）",  62)]
        public double BasicMedicalInsurance { get; set; }
        [Importable("失业保险费（个人）",  63)]
        public double UnemploymentInsurance { get; set; }
        [Importable("验算（个人）",  64)]
        public double Check { get; set; }
        [Importable("公积金（个人）",  65)]
        public double ProvidentFund { get; set; }
    }
}
