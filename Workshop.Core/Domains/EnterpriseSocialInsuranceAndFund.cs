namespace Workshop.Core.Domains
{
    public class EnterpriseSocialInsuranceAndFund : BaseDomainInfo
    {
        public string SocialInsuranceEnterprise { get; set; }
        public string SupplementarySocialInsuranceEnterprise { get; set; }
        public string ProvidentFundEnterprise { get; set; }
        public string SupplementaryProvidentFundEnterprise { get; set; }
        public string UnionFeeEnterprise { get; set; }
        public string SupplementaryUnionFeeDeductEnterprise { get; set; }
        public string Sum { get; set; }
    }
}