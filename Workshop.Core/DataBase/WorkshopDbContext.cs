using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domains;

namespace Workshop.Core.DataBase
{
    public class WorkshopDbContext:DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAccount> EmployeeAccount { get; set; }
        public DbSet<EmployeeSalay> SalayInfo { get; set; }
        public DbSet<EmployeeSocialInsuranceAndFund> EmployeeSocialInsuranceAndFund { get; set; }
        public DbSet<EnterpriseSocialInsuranceAndFund> EnterpriseSocialInsuranceAndFund { get; set; }
        public DbSet<EmployeeSocialInsuranceDetail> EmployeeSocialInsuranceDetail { get; set; }

        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }

        
    }
}
