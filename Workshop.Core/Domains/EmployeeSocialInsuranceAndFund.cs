namespace Workshop.Core.Domains
{
    public class EmployeeSocialInsuranceAndFund : BaseDomainInfo
    {
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