using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool.Core.EntityProxy
{
    public class EntityProxyContainer
    {
        //单例模式
        private static EntityProxyContainer instance;

        public static EntityProxyContainer Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityProxyContainer();
                }
                return instance;
            }
        }

        private EntityProxyContainer() { }

        private Assembly entityAss;

        public void Init()
        {
            var entityProxyGenerator = new EntityProxyGenerator();
            entityProxyGenerator.Process();
            this.entityAss = entityProxyGenerator.GetEntityProxyAssembly();
        }

        public void Init(string patternFilePath)
        {
            var entityProxyGenerator = new EntityProxyGenerator(patternFilePath);
            entityProxyGenerator.Process();
            this.entityAss = entityProxyGenerator.GetEntityProxyAssembly();
        }

        public Type EntityType =>
            this.entityAss.GetType("ExcelPatternTool.Core.Entites.ExcelEntity");
    }
}
