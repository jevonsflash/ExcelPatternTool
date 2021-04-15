namespace Workshop.Model.Dto
{
    public class EmployeeAccountDto : BaseDto
    {
        public string AccountNum { get; set; }

        public string AccountBankAlias { get; set; }
        public string AccountBankLoc { get; set; }
        public string AccountBankName { get; set; }

        public string SocialInsuranceNum { get; set; }

    }
}