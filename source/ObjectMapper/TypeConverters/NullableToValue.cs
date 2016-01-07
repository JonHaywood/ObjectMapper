using System;

namespace ObjectMapper.TypeConverters
{
    public class NullableToValue : ITypeConverter
    {
        public bool CanConvert(Type sourceType, Type destinationType)
        {
            var underlyingType = Nullable.GetUnderlyingType(sourceType);
            return underlyingType != null && underlyingType == destinationType;
        }

        public object Convert(object source)
        {
            return source ?? null;
        }
    }
}
