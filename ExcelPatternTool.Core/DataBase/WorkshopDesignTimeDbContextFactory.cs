using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExcelPatternTool.Core.DataBase
{

    public class ExcelPatternToolDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExcelPatternToolDbContext>
    {
        public ExcelPatternToolDbContext CreateDbContext(string[] args)
        {
            var connectionString = @"Data Source=mato.db";
            var contextOptions = new DbContextOptionsBuilder<ExcelPatternToolDbContext>()
                .UseSqlite(connectionString)
                .Options;

            return new ExcelPatternToolDbContext(contextOptions);
        }
    }
}
