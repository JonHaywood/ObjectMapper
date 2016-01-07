using System;

namespace ObjectMapper
{
    public abstract class ObjectMap
    {
        public abstract Type SourceType { get; }
        public abstract Type DestinationType { get; }
        public abstract object Map(IObjectMapperContext objectMapper, object source);
    }

    public abstract class ObjectMap<TSource, TDestination> : ObjectMap
    {
        public override Type SourceType
        {
            get { return typeof (TSource); }
        }

        public override Type DestinationType
        {
            get { return typeof (TDestination); }
        }

        public override object Map(IObjectMapperContext objectMapper, object source)
        {
            return MapObject(objectMapper, (TSource)source);
        }

        protected abstract TDestination MapObject(IObjectMapperContext objectMapper, TSource source);
    }
}
