using System;
using System.ComponentModel;
using Workshop.Core.Excel.Core.AdvancedTypes;

namespace Workshop.Core.Domains
{
    public class EmployeeSocialInsuranceAndFund : BaseDomainInfo
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DisplayName("社保个人")]
        public double SocialInsurancePersonal { get; set; }
        [DisplayName("补社保个人")]
        public double SupplementarySocialInsurancePersonal { get; set; }
        [DisplayName("公积金个人")]
        public double ProvidentFundPersonal { get; set; }
        [DisplayName("补公积金个人")]
        public double SupplementaryProvidentFundPersonal { get; set; }
        [DisplayName("税后补退")]
        public double BeforeFillingOut { get; set; }
        [DisplayName("个人所得税")]
        public double PersonalIncomeTax { get; set; }
        [DisplayName("工会费个人")]
        public double UnionFeePersonal { get; set; }
        [DisplayName("补扣工会费个人")]
        public double SupplementaryUnionFeeDeductPersonal { get; set; }
        [DisplayName("补扣福利商保")]
        public double SupplementaryCommercialInsuranceDeduct { get; set; }
        [DisplayName("实发薪金")]
        public FullAdvancedType<double> Sum { get; set; }
    }
}