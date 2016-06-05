using System;
using System.Collections.Generic;
using ObjectMapper.MappingRules;
using ObjectMapper.TypeConverters;

namespace ObjectMapper
{
    public static class ObjectMapper
    {
        public static IObjectMapperInstance Instance = new ObjectMapperInstance();

        public static void AddMap<TSource, TDestination>(Func<IObjectMapperContext, TSource, TDestination> mappingFunc)
        {
            Instance.AddMap(mappingFunc);
        }

        public static void AddMap<TObjectMap>() where TObjectMap : ObjectMap, new()
        {
            Instance.AddMap<TObjectMap>();
        }

        public static void AddMap(ObjectMap objectMap)
        {
            Instance.AddMap(objectMap);
        }

        public static void WithRule(IMappingRule rule)
        {
            Instance.WithRule(rule);
        }

        public static void AddConverter(params ITypeConverter[] typeConverter)
        {
            Instance.AddConverter(typeConverter);
        }

        public static void RegisterModule(ObjectMapperModule module)
        {
            Instance.RegisterModule(module);
        }

        public static void RegisterModule<T>() where T : ObjectMapperModule, new()
        {
            Instance.RegisterModule<T>();
        }

        public static TDestination Map<TDestination>(object source) where TDestination : new()
        {
            return Instance.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            return Instance.Map<TSource, TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination existingInstance)
        {
            return Instance.Map(source, existingInstance);
        }

        public static IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
            where TDestination : new()
        {
            return Instance.Map<TSource, TDestination>(source);
        }
    }
}
