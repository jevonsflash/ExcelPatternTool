using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Excel.Attributes;
using Workshop.Core.Excel.Core.AdvancedTypes;
using Workshop.Core.Excel.Models.Interfaces;

namespace Workshop.Core.Entites
{
    public class EmployeeEntity : IExcelEntity
    {
        [Exportable(ignore:true)]
        [Importable(ignore:true)]
        public long RowNumber { get; set; }
        [Exportable("年(Year)",0)]
        [Importable("年", 0)]
        public int Year { get; set; }
        [Exportable("月(Mounth)", 0)]
        [Importable("月", 1)]
        public int Mounth { get; set; }
        [Style(BackColor = "#66FFFF")]
        [Exportable("批次", 2)]
        [Importable("批次", 2)]
        public string Batch { get; set; }
        [Exportable("序号", 3)]
        [Importable("序号", 3)]
        public string SerialNum { get; set; }
        [Exportable("部门", 4)]
        [Importable("部门", 4)]
        public string Dept { get; set; }
        [Exportable("项目", 5)]
        [Importable("项目", 5)]
        public string Proj { get; set; }
        [Exportable("状态", 6)]
        [Importable("状态", 6)]
        public string State { get; set; }
        [Exportable("姓名", 7)]
        [Importable("姓名", 7)]
        public StyledType<string> Name { get; set; }
        [Exportable("身份证号码", 8)]
        [Importable("身份证号码", 8)]
        public StyledType<string> IDCard { get; set; }
        [Exportable("工资等级", 9)]
        [Importable("工资等级", 9)]
        public string Level { get; set; }
        [Exportable("工作性质", 10)]
        [Importable("工作性质", 10)]
        public string JobCate { get; set; }
        [Exportable("账号", 11)]
        [Importable("账号", 11)]
        public string AccountNum { get; set; }

        [Exportable("名称和地点", 12)]
        [Importable("名称和地点", 12)]
        public string AccountBankAlias { get; set; }
        [Exportable("银行名称", 13)]
        [Importable("银行名称", 13)]
        public FormulatedType<string> AccountBankName { get; set; }
        [Exportable("银行地点", 14)]
        [Importable("银行地点", 14)]
        public FormulatedType<string> AccountBankLoc { get; set; }

        [Exportable("个人社保号", 15)]
        [Importable("个人社保号", 15)]
        public string SocialInsuranceNum { get; set; }
        [Exportable("培训试用期工资",  16)]
        [Importable("培训试用期工资",  16)]
        public double ProbationSalary { get; set; }
        [Exportable("基本工资",  17)]
        [Importable("基本工资",  17)]
        public double BasicSalary { get; set; }
        [Exportable("技能工资",  18)]
        [Importable("技能工资",  18)]
        public double SkillSalary { get; set; }
        [Exportable("绩效奖金",  19)]
        [Importable("绩效奖金",  19)]
        public double PerformanceBonus { get; set; }
        [Exportable("岗位津贴",  20)]
        [Importable("岗位津贴",  20)]
        public double PostAllowance { get; set; }
        [Exportable("其他津贴",  21)]
        [Importable("其他津贴",  21)]
        public double OtherAllowances { get; set; }
        [Exportable("销售奖励",  22)]
        [Importable("销售奖励",  22)]
        public double SalesBonus { get; set; }
        [Exportable("奖金1",  23)]
        [Importable("奖金1",  23)]
        public double Bonus1 { get; set; }
        [Exportable("奖金2",  24)]
        [Importable("奖金2",  24)]
        public double Bonus2 { get; set; }
        [Exportable("绩效奖励/扣罚",  25)]
        [Importable("绩效奖励/扣罚",  25)]
        public double PerformanceRewards { get; set; }
        [Exportable("绩效扣罚",  26)]
        [Importable("绩效扣罚",  26)]
        public double PerformanceDeduct { get; set; }
        [Exportable("夜班费",  27)]
        [Importable("夜班费",  27)]
        public double NightAllowances { get; set; }
        [Exportable("普通加班费",  28)]
        [Importable("普通加班费",  28)]
        public double OrdinaryOvertime { get; set; }
        [Exportable("节日加班费",  29)]
        [Importable("节日加班费",  29)]
        public double HolidayOvertime { get; set; }
        [Exportable("年限",  30)]
        [Importable("年限",  30)]
        public double AgeBonus { get; set; }
        [Exportable("考勤扣",  31)]
        [Importable("考勤扣",  31)]
        public double AttendanceDeduct { get; set; }
        [Exportable("月全勤",  32)]
        [Importable("月全勤",  32)]
        public double MonthlyAttendance { get; set; }
        [Exportable("季度全勤",  33)]
        [Importable("季度全勤",  33)]
        public double QuarterlyAttendance { get; set; }
        [Exportable("其他应发",  34)]
        [Importable("其他应发",  34)]
        public double OtherRewards { get; set; }
        [Exportable("其他应扣",  35)]
        [Importable("其他应扣",  35)]
        public double OtherDeduct { get; set; }
        [Exportable("补发",  36)]
        [Importable("补发",  36)]
        public double SupplementaryRewards { get; set; }
        [Exportable("补扣",  37)]
        [Importable("补扣",  37)]
        public double SupplementaryDeduct { get; set; }
        [Exportable("兼职工资",  38)]
        [Importable("兼职工资",  38)]
        public double ParttimeSalary { get; set; }
        [Exportable("应发合计",  39)]
        [Importable("应发合计",  39)]
        public FullAdvancedType<double> Sum { get; set; }
        [Exportable("税后补扣\r\n(宿舍费) ",  40)]
        [Importable("税后补扣\r\n(宿舍费) ",  40)]
        public double HostelAllowances { get; set; }
        [Exportable("税后补扣\r\n(餐费) ",  41)]
        [Importable("税后补扣\r\n(餐费) ",  41)]
        public double MealAllowances { get; set; }
        [Exportable("临时计税",  42)]
        [Importable("临时计税",  42)]
        public double TemporaryTax { get; set; }
        [Exportable("应发+临时计税项目",  43)]
        [Importable("应发+临时计税项目",  43)]
        public FullAdvancedType<double> SumWithTemporaryTax { get; set; }
        [Exportable("社保个人",  44)]
        [Importable("社保个人",  44)]
        public double SocialInsurancePersonal { get; set; }
        [Exportable("补社保个人",  45)]
        [Importable("补社保个人",  45)]
        public double SupplementarySocialInsurancePersonal { get; set; }
        [Exportable("公积金个人",  46)]
        [Importable("公积金个人",  46)]
        public double ProvidentFundPersonal { get; set; }
        [Exportable("补公积金个人",  47)]
        [Importable("补公积金个人",  47)]
        public double SupplementaryProvidentFundPersonal { get; set; }
        [Exportable("税后补退",  48)]
        [Importable("税后补退",  48)]
        public double BeforeFillingOut { get; set; }
        [Exportable("个人所得税",  49)]
        [Importable("个人所得税",  49)]
        public double PersonalIncomeTax { get; set; }
        [Exportable("工会费个人",  50)]
        [Importable("工会费个人",  50)]
        public double UnionFeePersonal { get; set; }
        [Exportable("补扣工会费个人",  51)]
        [Importable("补扣工会费个人",  51)]
        public double SupplementaryUnionFeeDeductPersonal { get; set; }
        [Exportable("补扣福利商保",  52)]
        [Importable("补扣福利商保",  52)]
        public double SupplementaryCommercialInsuranceDeduct { get; set; }
        [Exportable("实发薪金",  53)]
        [Importable("实发薪金",  53)]
        public FullAdvancedType<double> Sum1 { get; set; }
        [Exportable("社保企业",  54)]
        [Importable("社保企业",  54)]
        public double SocialInsuranceEnterprise { get; set; }
        [Exportable("补社保企业",  55)]
        [Importable("补社保企业",  55)]
        public double SupplementarySocialInsuranceEnterprise { get; set; }
        [Exportable("公积金企业",  56)]
        [Importable("公积金企业",  56)]
        public double ProvidentFundEnterprise { get; set; }
        [Exportable("补公积金企业",  57)]
        [Importable("补公积金企业",  57)]
        public double SupplementaryProvidentFundEnterprise { get; set; }
        [Exportable("工会费企业",  58)]
        [Importable("工会费企业",  58)]
        public double UnionFeeEnterprise { get; set; }
        [Exportable("补扣工会费企业",  59)]
        [Importable("补扣工会费企业",  59)]
        public double SupplementaryUnionFeeDeductEnterprise { get; set; }
        [Exportable("总支付",  60)]
        [Importable("总支付",  60)]
        public FullAdvancedType<double> Sum2 { get; set; }
        [Exportable("基本养老险保险费（个人）",  61)]
        [Importable("基本养老险保险费（个人）",  61)]
        public double BasicOldAgeInsurance { get; set; }
        [Exportable("基本医疗保险费（个人）",  62)]
        [Importable("基本医疗保险费（个人）",  62)]
        public double BasicMedicalInsurance { get; set; }
        [Exportable("失业保险费（个人）",  63)]
        [Importable("失业保险费（个人）",  63)]
        public double UnemploymentInsurance { get; set; }
        [Exportable("验算（个人）",  64)]
        [Importable("验算（个人）",  64)]
        public FullAdvancedType<double> Check { get; set; }
        [Exportable("公积金（个人）",  65)]
        [Importable("公积金（个人）",  65)]
        public FullAdvancedType<double> ProvidentFund { get; set; }
    }
}
