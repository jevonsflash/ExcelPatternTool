using ExcelPatternTool.Core.DataBase;
using ExcelPatternTool.Core.EntityProxy;
using ExcelPatternTool.Core.Excel.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool
{
    public class DbProcessor
    {
        private static DbContextFactory dbContextFactory;

        public static void Init()
        {
            dbContextFactory = new DbContextFactory();

        }

        public static void ExportToDb(List<IExcelEntity> entities, string dbtype, string connectingString)
        {
            var odInfos = entities.ToList();
            if (odInfos.Count > 0)
            {
                if (string.IsNullOrEmpty(connectingString))
                {
                    return;
                }
                using (var dbcontext = dbContextFactory.CreateExcelPatternToolDbContext(connectingString, dbtype))
                {
                    dbcontext.AddRange(odInfos);
                    dbcontext.SaveChanges();
                }
            }
        }

        public static List<IExcelEntity> ImportFromDb(Type entityType, string dbtype, string connectingString)
        {
            using (var dbcontext = dbContextFactory.CreateExcelPatternToolDbContext(connectingString, dbtype))
            {
                var dbset = dbcontext.GetDbSet(entityType);
                return ((IEnumerable<IExcelEntity>)dbset).ToList();
            }
        }


    }
}
