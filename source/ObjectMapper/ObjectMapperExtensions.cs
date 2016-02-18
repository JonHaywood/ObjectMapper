using ObjectMapper.TypeConverters;

namespace ObjectMapper
{
    /// <summary>
    /// Convenience extensions methods for ObjectMapper.
    /// </summary>
    public static class ObjectMapperExtensions
    {
        public static IObjectMapperInstance AddNullableConverters(this IObjectMapperInstance objectMapper)
        {
            objectMapper.AddConverter(new NullableToValue(), new ValueToNullable());
            return objectMapper;
        }

        public static IObjectMapperInstance AddListAndArrayConverters(this IObjectMapperInstance objectMapper)
        {
            objectMapper.AddConverter(new ListToArray(), new ArrayToList());
            return objectMapper;
        }
    }
}
