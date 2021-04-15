using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Workshop.Helper
{
   public  static class AutoMapperHelper
    {
        /// <summary>
        ///  单个对象映射
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(this TSource obj)
        {
            if (obj == null) return default(TDestination);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var Mapper=new Mapper(config);
           
            return Mapper.Map<TDestination>(obj);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            if (source == null) return null;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var Mapper = new Mapper(config);
            return Mapper.Map<List<TDestination>>(source);
        }

    }
}
