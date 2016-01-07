using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectMapper.MappingRules
{
    /// <summary>
    /// Mapping rule that will match by name and type but also 'flatten' objects.
    /// That is, map property 'X.a.b.c' to property 'X.abc'.
    /// </summary>
    public class Flattening : MatchingNameAndType
    {
        protected override void HandleNoMatchingPropertyName(IObjectMapperContext mapperContext, PropertyInfo sourceProperty, object source,
            object destination)
        {
            // if there's not a matching name and this property isn't something we can further
            // inspect then there's nothing to do
            if (sourceProperty.PropertyType.IsValueType || sourceProperty.PropertyType.IsPrimitive || sourceProperty.PropertyType == typeof(String))
                return;

            var sourcePropertyValue = sourceProperty.GetValue(source, null);

            // if the source is null we can't inspect it further
            if (sourcePropertyValue == null)
                return;

            // find all the flattened matches and set them
            var subProperties = ReflectionUtils.GetProperties(sourcePropertyValue);
            var destinationProperties = ReflectionUtils.GetProperties(destination).ToList();

            foreach (var subProperty in subProperties)
            {
                FindAndSetFlattenedMatches(mapperContext, sourceProperty.Name, subProperty, sourcePropertyValue, destinationProperties, destination);
            }            
        }

        private void FindAndSetFlattenedMatches(IObjectMapperContext mapperContext, string prefix, PropertyInfo property, object obj, List<PropertyInfo> destinationProperties, object destination)
        {
            var propertyValue = property.GetValue(obj, null);
            if (propertyValue == null)
                return;

            // look for a matching destination property
            var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == prefix + property.Name && p.CanWrite && p.GetSetMethod() != null);
            if (destinationProperty != null)
            {
                SetDestinationPropertyValue(mapperContext, property, propertyValue, destinationProperty, destination);
            }

            var propertyValueType = propertyValue.GetType();

            // make sure we can inspect this further
            if (propertyValueType.IsValueType || propertyValueType.IsPrimitive || propertyValueType == typeof(String))
                return;

            var subProperties = ReflectionUtils.GetProperties(propertyValueType);
            foreach (var subProperty in subProperties)
            {
                if (subProperty.CanRead && subProperty.GetGetMethod() != null)
                    FindAndSetFlattenedMatches(mapperContext, prefix + property.Name, subProperty, propertyValue, destinationProperties, destination);
            }
        }
    }
}
