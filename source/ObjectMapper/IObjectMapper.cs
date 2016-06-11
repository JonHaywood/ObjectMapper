using System.Collections;
using System.Collections.Generic;

namespace ObjectMapper
{
    public interface IObjectMapper
    {        
        TDestination Map<TDestination>(object source) where TDestination : new();
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : new();
        TDestination Map<TSource, TDestination>(TSource source, TDestination existingInstance);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source) where TDestination : new();
        IEnumerable<TDestination> Map<TDestination>(IEnumerable source) where TDestination : new();
    }
}