using System;
using System.ComponentModel;

namespace Workshop.Core.Domains
{
    public class EmployeeAccount : BaseDomainInfo
    {

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [DisplayName("账号")]
        public string AccountNum { get; set; }

        [DisplayName("名称和地点")]
        public string AccountBankAlias { get; set; }
        [DisplayName("银行名称")]
        public string AccountBankName { get; set; }
        [DisplayName("银行地点")]
        public string AccountBankLoc { get; set; }

        [DisplayName("个人社保号")]
        public string SocialInsuranceNum { get; set; }

    }
}