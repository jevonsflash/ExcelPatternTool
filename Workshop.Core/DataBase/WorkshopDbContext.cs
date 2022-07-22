using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domains;
using Workshop.Core.Excel.Core.AdvancedTypes;

namespace Workshop.Core.DataBase
{
    public class WorkshopDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAccount> EmployeeAccount { get; set; }
        public DbSet<EmployeeSalay> SalayInfo { get; set; }
        public DbSet<EmployeeSocialInsuranceAndFund> EmployeeSocialInsuranceAndFund { get; set; }
        public DbSet<EnterpriseSocialInsuranceAndFund> EnterpriseSocialInsuranceAndFund { get; set; }
        public DbSet<EmployeeSocialInsuranceDetail> EmployeeSocialInsuranceDetail { get; set; }
        public DbSet<Log> Log { get; set; }

        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
            .Property(e => e.Name)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<StyledType<string>>(v)
             );
            modelBuilder.Entity<Employee>()
           .Property(e => e.IDCard)
           .HasConversion(
               v => JsonConvert.SerializeObject(v),
               v => JsonConvert.DeserializeObject<StyledType<string>>(v)
            );

            modelBuilder.Entity<EmployeeAccount>()
           .Property(e => e.AccountBankName)
           .HasConversion(
               v => JsonConvert.SerializeObject(v),
               v => JsonConvert.DeserializeObject<FormulatedType<string>>(v)
            );
            modelBuilder.Entity<EmployeeAccount>()
          .Property(e => e.AccountBankLoc)
          .HasConversion(
              v => JsonConvert.SerializeObject(v),
              v => JsonConvert.DeserializeObject<FormulatedType<string>>(v)
           );

            modelBuilder.Entity<EmployeeSalay>()
         .Property(e => e.Sum)
         .HasConversion(
             v => JsonConvert.SerializeObject(v),
             v => JsonConvert.DeserializeObject<FullAdvancedType<double>>(v)
          );

            modelBuilder.Entity<EmployeeSalay>()
        .Property(e => e.SumWithTemporaryTax)
        .HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<FullAdvancedType<double>>(v)
         );

            modelBuilder.Entity<EmployeeSocialInsuranceAndFund>()
       .Property(e => e.Sum)
       .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<FullAdvancedType<double>>(v)
        );
            modelBuilder.Entity<EnterpriseSocialInsuranceAndFund>()
       .Property(e => e.Sum)
       .HasConversion(
           v => JsonConvert.SerializeObject(v),
           v => JsonConvert.DeserializeObject<FullAdvancedType<double>>(v)
        );

            modelBuilder.Entity<EmployeeSocialInsuranceDetail>()
      .Property(e => e.Check)
      .HasConversion(
          v => JsonConvert.SerializeObject(v),
          v => JsonConvert.DeserializeObject<FullAdvancedType<double>>(v)
       );


            modelBuilder.Entity<EmployeeSocialInsuranceDetail>()
      .Property(e => e.ProvidentFund)
      .HasConversion(
          v => JsonConvert.SerializeObject(v),
          v => JsonConvert.DeserializeObject<FullAdvancedType<double>>(v)
       );
            base.OnModelCreating(modelBuilder);
        }
    }
}
