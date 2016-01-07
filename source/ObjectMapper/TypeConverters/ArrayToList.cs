using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectMapper.TypeConverters
{
    public class ArrayToList : ITypeConverter
    {
        public bool CanConvert(Type sourceType, Type destinationType)
        {
            if (!sourceType.IsArray)
                return false;
            if (destinationType.IsGenericTypeDefinition && destinationType.GetGenericTypeDefinition() != typeof(List<>))
                return false;

            var sourceElementType = sourceType.GetElementType();
            var destinationElementType = destinationType.GetGenericArguments().Single();

            return sourceElementType == destinationElementType;
        }

        public object Convert(object source)
        {
            var sourceElementType = source.GetType().GetElementType();

            MethodInfo castMethod = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new[] { sourceElementType });
            MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(new[] { sourceElementType });

            var castedObjectEnum = castMethod.Invoke(null, new object[] { source });
            var castedObject = toArrayMethod.Invoke(null, new object[] { castedObjectEnum });

            return castedObject;
        }
    }
}
