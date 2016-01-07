namespace ObjectMapper.MappingRules
{
    public abstract class MappingRule : IMappingRule
    {
        public object Map(IObjectMapperContext mapperContext, object source, object destination)
        {
            ApplyMap(mapperContext, source, destination);
            return destination;
        }

        protected abstract void ApplyMap(IObjectMapperContext mapperContext, object source, object destination);        
    };
}
