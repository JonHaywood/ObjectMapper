using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectMapper.MappingRules
{
    /// <summary>
    /// Mapping rule that will match by name and type but also 'unflatten' objects.
    /// That is, map property 'X.abc' to property 'X.a.b.c'.
    /// </summary>
    public class UnFlattening : MatchingNameAndType
    {
        protected override void HandleNoMatchingPropertyName(IObjectMapperContext mapperContext, PropertyInfo sourceProperty, object source, object destination)
        {
            var propertyPath = FindUnFlattenedMatch(sourceProperty.Name, destination);
            if (propertyPath == null)
                return;
            
            object tempObj = destination;
            object sourcePropertyValue = sourceProperty.GetValue(source, null);

            // walk the path of properties up to the last one
            for (int i = 0; i < propertyPath.Count; i++)
            {                
                // we're on the last property
                if (i == propertyPath.Count - 1)
                    break;

                var propertyInfo = propertyPath[i];
                var value = propertyInfo.GetValue(tempObj, null);

                // the value could be null if not created yet - if so create it and set it
                if (value == null)
                {
                    value = Activator.CreateInstance(propertyInfo.PropertyType);
                    propertyInfo.SetValue(tempObj, value, null);
                }

                tempObj = value;
            }

            // set the value on final property
            var destinationProperty = propertyPath.Last();            
            SetDestinationPropertyValue(mapperContext, sourceProperty, sourcePropertyValue, destinationProperty, tempObj);
        }

        private List<PropertyInfo> FindUnFlattenedMatch(string sourcePropertyName, object destination)
        {
            var properties = ReflectionUtils.GetProperties(destination);
            var stack = new Stack<PropertyInfo>();

            foreach (var property in properties)
            {
                stack.Push(property);
                if (FindUnFlattenedMatchRecursive(sourcePropertyName, stack, property.PropertyType))
                    return stack.Reverse().ToList();
                stack.Pop();
            }

            return null;
        }

        private bool FindUnFlattenedMatchRecursive(string sourcePropertyName, Stack<PropertyInfo> path, Type targetType)
        {
            var properties = ReflectionUtils.GetProperties(targetType);
            var prefix = string.Join(string.Empty, path.Reverse().Select(p => p.Name));

            if (targetType.IsValueType || targetType.IsPrimitive || targetType == typeof(String))
                return false;

            foreach (var property in properties)
            {
                // make sure we can write to it
                if (ExcludedProperties.Contains(property.Name) || !property.CanWrite || property.GetSetMethod() == null)
                    continue;
                
                bool isMatch = IsCaseSensitive
                    ? prefix + property.Name == sourcePropertyName
                    : (prefix + property.Name).Equals(sourcePropertyName, StringComparison.InvariantCultureIgnoreCase);

                // found it!
                if (isMatch)
                {
                    path.Push(property);
                    return true;
                }

                // if we don't have a match and it's a value type then we're done, continue searching
                if (property.PropertyType.IsValueType)
                    continue;

                path.Push(property);

                // keep going down
                var result = FindUnFlattenedMatchRecursive(sourcePropertyName, path, property.PropertyType);
                if (result)
                    return true;

                path.Pop();
            }
            
            return false;
        }
    }
}
