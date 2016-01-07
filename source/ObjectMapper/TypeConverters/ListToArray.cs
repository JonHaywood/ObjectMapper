using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectMapper.TypeConverters
{
    public class ListToArray : ITypeConverter
    {
        public bool CanConvert(Type sourceType, Type destinationType)
        {
            if (sourceType.IsGenericTypeDefinition && sourceType.GetGenericTypeDefinition() != typeof (List<>))
                return false;
            if (!destinationType.IsArray)
                return false;

            var sourceElementType = sourceType.GetGenericArguments().Single();
            var destinationElementType = destinationType.GetElementType();

            return sourceElementType == destinationElementType;
        }

        public object Convert(object source)
        {
            var sourceElementType = source.GetType().GetGenericArguments().Single();

            MethodInfo castMethod = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new[] { sourceElementType });
            MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray").MakeGenericMethod(new[] { sourceElementType });

            var castedObjectEnum = castMethod.Invoke(null, new object[] { source });
            var castedObject = toArrayMethod.Invoke(null, new object[] { castedObjectEnum });

            return castedObject;
        }
    }
}
