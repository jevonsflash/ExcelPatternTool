using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool.Core.DataBase
{
    public class DbContextFactory
    {
        public ExcelPatternToolDbContext CreateExcelPatternToolDbContext(string connectionString, string use)
        {
            ExcelPatternToolDbContext _dataContext;
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ExcelPatternToolDbContext>();
            var contextOptions = use.ToLower() switch
            {
                "sqlite" => dbContextOptionsBuilder.UseSqlite(connectionString).Options,
                "sqlserver" => dbContextOptionsBuilder.UseSqlServer(connectionString).Options,
                "mysql" => dbContextOptionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29))).Options,
                _ => throw new NotImplementedException("暂不支持"+use)
            };


            _dataContext= new ExcelPatternToolDbContext(contextOptions);

            return _dataContext;
        }
    }
}
