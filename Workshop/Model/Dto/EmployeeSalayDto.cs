namespace Workshop.Model.Dto
{
    public class EmployeeSalayDto : BaseDto
    {
        //培训试用期工资
        public string ProbationSalary { get; set; }
        public string BasicSalary { get; set; }
        public string SkillSalary { get; set; }
        public string PerformanceBonus { get; set; }
        public string PostAllowance { get; set; }
        public string OtherAllowances { get; set; }
        public string SalesBonus { get; set; }
        public string Bonus1 { get; set; }
        public string Bonus2 { get; set; }
        public string PerformanceRewards { get; set; }
        public string PerformanceDeduct { get; set; }
        public string NightAllowances { get; set; }
        public string OrdinaryOvertime { get; set; }
        public string HolidayOvertime { get; set; }
        public string AgeBonus { get; set; }
        public string AttendanceDeduct { get; set; }
        public string MonthlyAttendance { get; set; }
        public string QuarterlyAttendance { get; set; }
        public string OtherRewards { get; set; }
        public string OtherDeduct { get; set; }
        public string SupplementaryRewards { get; set; }
        public string SupplementaryDeduct { get; set; }
        public string ParttimeSalary { get; set; }
        public string Sum { get; set; }
        public string HostelAllowances { get; set; }
        public string MealAllowances { get; set; }
    }
}