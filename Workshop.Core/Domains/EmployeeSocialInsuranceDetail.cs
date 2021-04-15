namespace Workshop.Core.Domains
{
    public class EmployeeSocialInsuranceDetail : BaseDomainInfo
    {
        public string BasicOldAgeInsurance { get; set; }
        public string BasicMedicalInsurance { get; set; }
        public string UnemploymentInsurance { get; set; }
        public string Check { get; set; }
        public string ProvidentFund { get; set; }
    }
}