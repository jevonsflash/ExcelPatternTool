using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Workshop.Core.DataBase
{

    public class WorkshopDesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkshopDbContext>
    {
        public WorkshopDbContext CreateDbContext(string[] args)
        {
            var connectionString = @"Data Source=mato.db";
            var contextOptions = new DbContextOptionsBuilder<WorkshopDbContext>()
                .UseSqlite(connectionString)
                .Options;

            return new WorkshopDbContext(contextOptions);
        }
    }
}
