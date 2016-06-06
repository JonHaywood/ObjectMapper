using System;

namespace ObjectMapper.TypeConverters
{
    public class FunctionTypeConverter<TSource, TDestination> : TypeConverter<TSource, TDestination>
    {
        private readonly Func<TSource, TDestination> convertFunc;

        public FunctionTypeConverter(Func<TSource, TDestination> convertFunc)
        {
            this.convertFunc = convertFunc;
        }

        protected override TDestination ConvertSource(TSource source)
        {
            return convertFunc(source);
        }
    }
}
