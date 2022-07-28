using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExcelPatternTool.Core.Excel.Core.AdvancedTypes;
using ExcelPatternTool.Core.EntityProxy;

namespace ExcelPatternTool.Core.DataBase
{
    public class ExcelPatternToolDbContext : DbContext
    {

        public ExcelPatternToolDbContext(DbContextOptions<ExcelPatternToolDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //todo:replace with proxyclass
            Type entityType = EntityProxyContainer.Current.EntityType;


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
