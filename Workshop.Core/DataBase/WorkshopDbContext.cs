using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}
