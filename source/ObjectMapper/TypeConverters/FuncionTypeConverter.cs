using System;

namespace ObjectMapper.TypeConverters
{
    public class FuncionTypeConverter<TSource, TDestination> : ITypeConverter
    {
        private readonly Func<TSource, TDestination> convertFunc;

        public FuncionTypeConverter(Func<TSource, TDestination> convertFunc)
        {
            this.convertFunc = convertFunc;
        }

        public bool CanConvert(Type sourceType, Type destinationType)
        {
            return sourceType == typeof (TSource) && destinationType == typeof (TDestination);
        }

        public object Convert(object source)
        {
            return convertFunc((TSource)source);
        }
    }
}
