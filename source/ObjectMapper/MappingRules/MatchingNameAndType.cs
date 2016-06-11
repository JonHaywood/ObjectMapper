using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectMapper.MappingRules
{
    public class MatchingNameAndType : MappingRule
    {
        private readonly List<string> excludedProperties;

        public MatchingNameAndType(params string[] excludedProperties)
        {
            this.excludedProperties = excludedProperties != null
                ? new List<string>(excludedProperties)
                : new List<string>();
            IsCaseSensitive = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether name matching is case sensitive. Is true by default.
        /// </summary>
        public bool IsCaseSensitive { get; set; }

        /// <summary>
        /// Gets the excluded properties.
        /// </summary>
        public IReadOnlyList<string> ExcludedProperties
        {
            get { return excludedProperties; }
        }

        /// <summary>
        /// Sets the properties to exclude when matching.
        /// </summary>
        /// <param name="propertiesToExclude">The properties to exclude.</param>
        public void ExcludeProperties(params string[] propertiesToExclude)
        {
            excludedProperties.AddRange(propertiesToExclude);
        }

        protected override void ApplyMap(IObjectMapperContext mapperContext, object source, object destination)
        {
            var sourceProperties = ReflectionUtils.GetProperties(source);
            var destinationProperties = ReflectionUtils.GetProperties(destination);

            foreach (var sourceProperty in sourceProperties)
            {
                // make sure this source property is something we can read and isn't excluded
                if (!IsSourcePropertyValid(sourceProperty)) 
                    continue;

                // find a matching destination property
                var destinationProperty = FindDestinationPropertyWithMatchingName(sourceProperty, destinationProperties);
                if (destinationProperty == null)
                {
                    HandleNoMatchingPropertyName(mapperContext, sourceProperty, source, destination);
                    continue;
                }

                // we have a matching name but make sure we can write to it
                if (!destinationProperty.CanWrite || destinationProperty.GetSetMethod() == null)
                    continue;

                var sourcePropertyValue = sourceProperty.GetValue(source, null);
                SetDestinationPropertyValue(mapperContext, sourceProperty, sourcePropertyValue, destinationProperty, destination);
            }
        }

        protected virtual bool IsSourcePropertyValid(PropertyInfo sourceProperty)
        {
            return sourceProperty.CanRead && sourceProperty.GetGetMethod() != null && !excludedProperties.Contains(sourceProperty.Name);
        }

        /// <summary>
        /// Sets the destination property value.
        /// </summary>
        /// <param name="mapperContext">The mapper context.</param>
        /// <param name="sourceProperty">The source property.</param>
        /// <param name="sourcePropertyValue">The source property value.</param>
        /// <param name="destinationProperty">The destination property.</param>
        /// <param name="destination">The destination object to set the value on.</param>
        protected virtual void SetDestinationPropertyValue(IObjectMapperContext mapperContext, PropertyInfo sourceProperty,
            object sourcePropertyValue, PropertyInfo destinationProperty, object destination)
        {
            // if the types match then just set the value it
            if (sourceProperty.PropertyType == destinationProperty.PropertyType)
            {
                destinationProperty.SetValue(destination, sourcePropertyValue, null);
            }
            else
            {
                HandlePropertiesWithDifferentTypes(mapperContext, sourceProperty, sourcePropertyValue, destinationProperty, destination);
            }
        }

        /// <summary>
        /// Handles the properties with different types.
        /// </summary>
        /// <param name="mapperContext">The mapper context.</param>
        /// <param name="sourceProperty">The source property.</param>
        /// <param name="sourcePropertyValue">The source property value.</param>
        /// <param name="destinationProperty">The destination property.</param>
        /// <param name="destination">The destination.</param>
        protected virtual void HandlePropertiesWithDifferentTypes(IObjectMapperContext mapperContext, PropertyInfo sourceProperty, object sourcePropertyValue, PropertyInfo destinationProperty, object destination)
        {
            // find a converter that can convert between types
            var typeConverter = mapperContext.TypeConverters.FirstOrDefault(
                t => t.CanConvert(sourceProperty.PropertyType, destinationProperty.PropertyType));

            if (typeConverter != null)
            {
                var convertedValue = typeConverter.Convert(sourcePropertyValue);
                destinationProperty.SetValue(destination, convertedValue, null);
                return;
            }

            // see if there's an objectmap that can convert between types
            var objectMap = mapperContext.Maps.FirstOrDefault(m =>
                m.SourceType == sourceProperty.PropertyType &&
                m.DestinationType == destinationProperty.PropertyType);
            if (objectMap == null)
                return;

            destinationProperty.SetValue(destination, objectMap.Map(mapperContext, sourcePropertyValue), null);
        }

        /// <summary>
        /// Handles the case when the property name does not have a match in the destination.
        /// </summary>
        /// <param name="mapperContext">The mapper context.</param>
        /// <param name="sourceProperty">The source property.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        protected virtual void HandleNoMatchingPropertyName(IObjectMapperContext mapperContext, PropertyInfo sourceProperty, object source, object destination)
        {}

        private PropertyInfo FindDestinationPropertyWithMatchingName(PropertyInfo sourceProperty, PropertyInfo[] destinationProperties)
        {
            // find a matching property by name
            return IsCaseSensitive
                ? destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name)
                : destinationProperties.FirstOrDefault(p => p.Name.Equals(sourceProperty.Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
