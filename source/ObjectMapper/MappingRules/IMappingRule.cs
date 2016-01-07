namespace ObjectMapper.MappingRules
{
    public interface IMappingRule
    {
        object Map(IObjectMapperContext mapperContext, object source, object destination);
    }
}