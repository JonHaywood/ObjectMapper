using System.Collections.Generic;

namespace ObjectMapper
{
    public interface IObjectMapper
    {        
        TDestination Map<TDestination>(object source) where TDestination : new();
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : new();
        TDestination Map<TSource, TDestination>(TSource source, TDestination existingInstance);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source) where TDestination : new();
    }
}