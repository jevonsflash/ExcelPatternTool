using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Core.Domains;
using Workshop.Core.Entites;
using Workshop.Core.Excel.Core.AdvancedTypes;

namespace Workshop.Core.DataBase
{
    public class WorkshopDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employee { get; set; }
        public DbSet<Log> Log { get; set; }

        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //todo:replace with proxyclass
            Type entityType = typeof(EmployeeEntity);

            base.OnModelCreating(modelBuilder);
            var entityTypeBuilder = modelBuilder
             .Entity(entityType);

            foreach (var prop in entityType.GetProperties())
            {
                var propType = prop.PropertyType;
                var propName = prop.Name;
                var gType = propType.GenericTypeArguments.FirstOrDefault();


                if (typeof(IAdvancedType).IsAssignableFrom(propType))
                {
                    entityTypeBuilder.Property(propName).HasConversion(new AdvancedTypeConverter(propType, gType));
                }

            }
        }
    }
}
