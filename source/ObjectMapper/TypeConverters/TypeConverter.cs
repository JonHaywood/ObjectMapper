using System;

namespace ObjectMapper.TypeConverters
{
    public abstract class TypeConverter<TSource, TDestination> : ITypeConverter
    {
        public bool CanConvert(Type sourceType, Type destinationType)
        {
            return sourceType == typeof (TSource) && destinationType == typeof (TDestination);
        }

        public object Convert(object source)
        {
            return ConvertSource((TSource)source);
        }

        protected abstract TDestination ConvertSource(TSource source);
    }
}
