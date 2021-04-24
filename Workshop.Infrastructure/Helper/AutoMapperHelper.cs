using System.Collections.Generic;
using AutoMapper;

namespace Workshop.Infrastructure.Helper
{
    public static class AutoMapperHelper
    {
        /// <summary>
        ///  单个对象映射
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(this TSource obj, MapperConfiguration config = null)
        {
            if (obj == null) return default(TDestination);
            if (config == null)
            {
                config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            }
            var Mapper = new Mapper(config);

            return Mapper.Map<TDestination>(obj);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source, MapperConfiguration config = null)
        {
            if (source == null) return null;
            if (config == null)
            {
                config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            }
            var Mapper = new Mapper(config);
            return Mapper.Map<List<TDestination>>(source);
        }

    }
}
