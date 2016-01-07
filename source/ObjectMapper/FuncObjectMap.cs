using System;

namespace ObjectMapper
{
    /// <summary>
    /// Wraps a passed in function in an ObjectMap instance.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    public class FuncObjectMap<TSource, TDestination> : ObjectMap<TSource, TDestination>
    {
        private readonly Func<IObjectMapperContext, TSource, TDestination> mappingFunc;

        public FuncObjectMap(Func<IObjectMapperContext, TSource, TDestination> mappingFunc)
        {
            this.mappingFunc = mappingFunc;
        }

        protected override TDestination MapObject(IObjectMapperContext objectMapper, TSource source)
        {
            return mappingFunc.Invoke(objectMapper, source);
        }
    }
}
