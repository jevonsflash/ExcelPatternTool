using System;
using System.ComponentModel;
using Workshop.Infrastructure.Core;

namespace Workshop.Core.Domains
{
    public class EnterpriseSocialInsuranceAndFund : BaseDomainInfo
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [DisplayName("社保企业")]
        public double SocialInsuranceEnterprise { get; set; }
        [DisplayName("补社保企业")]
        public double SupplementarySocialInsuranceEnterprise { get; set; }
        [DisplayName("公积金企业")]
        public double ProvidentFundEnterprise { get; set; }
        [DisplayName("补公积金企业")]
        public double SupplementaryProvidentFundEnterprise { get; set; }
        [DisplayName("工会费企业")]
        public double UnionFeeEnterprise { get; set; }
        [DisplayName("补扣工会费企业")]
        public double SupplementaryUnionFeeDeductEnterprise { get; set; }
        [DisplayName("总支付")]
        public FullAdvancedType<double> Sum { get; set; }
    }
}