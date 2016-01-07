using System;

namespace ObjectMapper.TypeConverters
{
    public interface ITypeConverter
    {
        bool CanConvert(Type sourceType, Type destinationType);

        object Convert(object source);
    }
}