using System;
using ObjectMapper.MappingRules;
using ObjectMapper.TypeConverters;

namespace ObjectMapper
{
    public interface IObjectMapperConfiguration
    {
        void AddMap<TSource, TDestination>(Func<IObjectMapperContext, TSource, TDestination> mappingFunc);
        void AddMap<TObjectMap>() where TObjectMap : ObjectMap, new();
        void AddMap(ObjectMap objectMap);
        void WithRule(IMappingRule rule);
        void AddConverter(params ITypeConverter[] typeConverter); 
    }
}