using Workshop.Infrastructure.ExcelHandler;

namespace Workshop.Core.Domains
{
    public class EmployeeAccount : BaseDomainInfo
    {
        [ExcelImport("账号", order = 11)]
        public string AccountNum { get; set; }

        [ExcelImport("名称和地点", order = 12)]
        public string AccountBankAlias { get; set; }
        [ExcelImport("银行名称", order = 13)]
        public string AccountBankName { get; set; }
        [ExcelImport("银行地点", order = 14)]
        public string AccountBankLoc { get; set; }

        [ExcelImport("个人社保号", order = 15)]
        public string SocialInsuranceNum { get; set; }

    }
}