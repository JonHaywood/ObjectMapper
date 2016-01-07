namespace ObjectMapper
{
    public interface IObjectMapper
    {        
        TDestination Map<TDestination>(object source) where TDestination : new();
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : new();
        TDestination Map<TSource, TDestination>(TSource source, TDestination existingInstance);
    }
}