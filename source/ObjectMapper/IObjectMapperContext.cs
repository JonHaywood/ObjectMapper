using System.Collections.Generic;
using ObjectMapper.TypeConverters;

namespace ObjectMapper
{
    public interface IObjectMapperContext
    {
        IReadOnlyCollection<ITypeConverter> TypeConverters { get; }
        IReadOnlyCollection<ObjectMap> Maps { get; }

        TDestination Map<TSource, TDestination>(TSource source) where TDestination : new();
        TDestination Map<TSource, TDestination>(TSource source, TDestination existingInstance);
    }
}