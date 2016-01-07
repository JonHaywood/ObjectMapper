using System;

namespace ObjectMapper.TypeConverters
{
    public class ValueToNullable : ITypeConverter
    {
        public bool CanConvert(Type sourceType, Type destinationType)
        {
            var underlyingType = Nullable.GetUnderlyingType(destinationType);
            return underlyingType != null && underlyingType == sourceType;
        }

        public object Convert(object source)
        {
            return source;
        }
    }
}
