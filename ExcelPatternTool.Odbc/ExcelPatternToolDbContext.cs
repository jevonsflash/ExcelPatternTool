using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* 项目“ExcelPatternTool.Odbc (net7.0)”的未合并的更改
在此之前:
using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;

namespace ExcelPatternTool.Core.DataBase
在此之后:
using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;
using ExcelPatternTool.Odbc;
using ExcelPatternTool;
using ExcelPatternTool.Core;
using ExcelPatternTool.Core.DataBase;

namespace ExcelPatternTool.Odbc
*/
using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;

namespace ExcelPatternTool.Odbc
{
    public class ExcelPatternToolDbContext : DbContext
    {
        public Type EntityType { get; set; }

        public ExcelPatternToolDbContext(DbContextOptions<ExcelPatternToolDbContext> options)
            : base(options)
        {
        }

        public ExcelPatternToolDbContext(DbContextOptions<ExcelPatternToolDbContext> options, Type entityType) : this(options)
        {
            EntityType = entityType;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            var entityTypeBuilder = modelBuilder
             .Entity(EntityType);

            foreach (var prop in EntityType.GetProperties())
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

        public object GetDbSet(Type entityType)
        {
            var method = GetType().GetMethod("Set", new Type[0]).MakeGenericMethod(entityType);
            return method.Invoke(this, new object[0]);
        }
    }
}
