using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Model.Dto;

namespace Workshop.Model
{
    public class EmployeeDto : BaseDto
    {
        public int Year { get; set; }
        public int Mounth { get; set; }
        public string Batch { get; set; }
        public string Dept { get; set; }
        public string Proj { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public string IDCard { get; set; }
        public string Level { get; set; }
        public string JobCate { get; set; }

        public EmployeeAccountDto EmployeeAccount { get; set; }
        public EmployeeSalayDto SalayInfo { get; set; }
        public EmployeeSocialInsuranceAndFundDto EmployeeSocialInsuranceAndFund { get; set; }
        public EnterpriseSocialInsuranceAndFundDto EnterpriseSocialInsuranceAndFund { get; set; }
        public EmployeeSocialInsuranceDetailDto EmployeeSocialInsuranceDetail { get; set; }
    }
}
